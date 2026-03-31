using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class TwatIt() : RedDwarfCardModel(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SmegPower>(),
        HoverTipFactory.FromPower<CowedPower>(),

    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(12m, ValueProp.Move),
        new PowerVar<SmegPower>(1m),
        new PowerVar<CowedPower>(50m),

    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
        "LISTER: Well, I say let's get out there and twat it! ", Owner.Creature, 3));

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        await PowerCmd.Apply<SmegPower>(
            cardPlay.Target,
            DynamicVars[Smeg].IntValue,
            Owner.Creature,
            this
        );

        await PowerCmd.Apply<CowedPower>(
           cardPlay.Target,
           DynamicVars[Cowed].IntValue,
           Owner.Creature,
           this
       );
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Cowed].UpgradeValueBy(25m);
        DynamicVars[Smeg].UpgradeValueBy(1m);
        DynamicVars.Damage.UpgradeValueBy(3m);

    }
}