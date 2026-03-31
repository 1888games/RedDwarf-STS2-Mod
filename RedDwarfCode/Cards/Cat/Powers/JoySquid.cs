using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class JoySquid(): RedDwarfCardModel(0, CardType.Power, CardRarity.Uncommon, TargetType.AllEnemies ,RedDwarfCharacter.CAT)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [

        ..base.CanonicalVars,
           new PowerVar<SelfEsteemPower>(4m),
           
        new DynamicVar(Cowed, 3)

        ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>(),
        HoverTipFactory.FromPower<SelfEsteemPower>(),

    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: Why do I keep making these...?", Owner.Creature, 2.5));

        await PlayerCmd.GainEnergy(DynamicVars[Cowed].IntValue, Owner);
        await CardPileCmd.Draw(choiceContext, DynamicVars[Cowed].IntValue, Owner);
        await PowerCmd.Apply<SelfEsteemPower>(CombatState.HittableEnemies, DynamicVars["SelfEsteemPower"].BaseValue, Owner.Creature, this);  
    }

    protected override void OnUpgrade() { 
    
        DynamicVars[Cowed].UpgradeValueBy(1);
    }
}
