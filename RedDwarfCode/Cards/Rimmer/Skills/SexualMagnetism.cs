
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class SexualMagnetism() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.RIMMER)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [.. base.CanonicalVars,
        new PowerVar<ConfidencePower>(4),
        new PowerVar<ParanoiaPower>(4)

     ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>(),
        HoverTipFactory.FromPower<ParanoiaPower>()
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ConfidencePower>(Owner.Creature, DynamicVars[Confidence].IntValue, Owner.Creature, this);
        await PowerCmd.Apply<ParanoiaPower>(Owner.Creature, DynamicVars[Paranoia].IntValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }


}