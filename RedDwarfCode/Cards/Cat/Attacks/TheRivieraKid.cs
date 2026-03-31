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


public sealed class RivieraKid() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy,RedDwarfCharacter.CAT)
{
 
    protected override IEnumerable<DynamicVar> CanonicalVars => [

        ..base.CanonicalVars,
        new DamageVar(6m, ValueProp.Move),
        new DynamicVar(Draw, 2),
        new DynamicVar(Confidence, 1)
        
        ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>(),
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null || cardPlay.Target.Monster == null) return;
        if (cardPlay.Target.Monster.IntendsToAttack == false) return;

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: This is a job for the Riviera Kid!", Owner.Creature, 2.5));


        await CardPileCmd.Draw(choiceContext, DynamicVars[Draw].IntValue, Owner);

        if (IsUpgraded)
        {
            await PowerCmd.Apply<ConfidencePower>(Owner.Creature, DynamicVars[Confidence].BaseValue, Owner.Creature, this);

        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m);
       // base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
