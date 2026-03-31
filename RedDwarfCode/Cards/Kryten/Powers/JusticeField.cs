
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

public sealed class JusticeField() : RedDwarfCardModel(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.KRYTEN)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [.. base.CanonicalVars, new PowerVar<MirrorPower>(1)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<MirrorPower>()
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
            "You are now entering the Justice Zone. Beyond this point, it is impossible to commit any act of injustice.", Owner.Creature, 4));

        await PowerCmd.Apply<MirrorPower>(Owner.Creature, DynamicVars[Mirror].IntValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {

        AddKeyword(CardKeyword.Retain);
    }
}