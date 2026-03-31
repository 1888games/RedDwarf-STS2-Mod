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


public sealed class FriedEgg() : RedDwarfCardModel(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.RIMMER, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SustenancePower>(),
        HoverTipFactory.FromPower<BlockedArteriesPower>(),
        HoverTipFactory.FromPower<RegularMealPower>(),

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new PowerVar<SustenancePower>(6m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<BlockedArteriesPower>(
            Owner.Creature,
            1,
            Owner.Creature,
            this
        );

        await PowerCmd.Apply<RegularMealPower>(
            Owner.Creature,
            DynamicVars[Sustenance].BaseValue,
            Owner.Creature,
            this
        );

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: I feel like I'm having a baby... ", Owner.Creature, 2.5));

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(2m);
    }
}