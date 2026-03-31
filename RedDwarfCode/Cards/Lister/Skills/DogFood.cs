using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class DogFood() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Rare, TargetType.Self, RedDwarfCharacter.LISTER, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SustenancePower>(),
      
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new PowerVar<SustenancePower>(7m),
        new EnergyVar(1),
        new DamageVar(5m, ValueProp.Unblockable),
        new CardsVar(2)

    ];

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
       CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars[Sustenance].IntValue,
            Owner.Creature,
            this
        );

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
"CONFIDENCE: Hey! It's the king! Mr. Beautiful!", Owner.Creature, 3));

        await PlayerCmd.GainEnergy(1, Owner);
        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), Owner.Creature, DynamicVars.Damage.BaseValue, ValueProp.Unblockable, Owner.Creature);
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(2m);
        //DynamicVars.Damage.UpgradeValueBy(-1);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}