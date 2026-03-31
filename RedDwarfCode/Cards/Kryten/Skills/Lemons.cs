
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class Lemons() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self, RedDwarfCharacter.KRYTEN)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [.. base.CanonicalVars];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        //HoverTipFactory.FromPower<MirrorPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(1, Owner);
        // await PowerCmd.Apply<MirrorPower>(Owner.Creature, DynamicVars[Mirror].IntValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {

        AddKeyword(CardKeyword.Retain);
    }

}      