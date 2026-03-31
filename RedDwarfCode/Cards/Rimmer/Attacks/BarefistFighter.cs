using Godot;
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


public sealed class BarefistFighter() : RedDwarfCardModel(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.RIMMER)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
         HoverTipFactory.FromPower<ConfidencePower>(),


    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(4m, ValueProp.Move),
        new DynamicVar(Confidence, 1m)
   
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: The name's Dangerous Dan McGrew - barefist fighter extraordinaire.", Owner.Creature, 2.5));

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        await PowerCmd.Apply<ConfidencePower>(
            Owner.Creature,
            DynamicVars[Confidence].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars[Confidence].UpgradeValueBy(1m);

    }
}