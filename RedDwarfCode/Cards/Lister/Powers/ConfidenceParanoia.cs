
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class ConfidenceParanoia() : RedDwarfCardModel(1, CardType.Power, CardRarity.Rare, TargetType.Self, RedDwarfCharacter.LISTER)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [.. base.CanonicalVars,
        new PowerVar<ConfidencePower>(4),
        new PowerVar<ParanoiaPower>(1)

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

        DynamicVars[Confidence].UpgradeValueBy(2);
        DynamicVars[Paranoia].UpgradeValueBy(1);
    }


}