using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class NasalIntuition() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.CAT, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<DexterityPower>(),
      
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new PowerVar<DexterityPower>(2m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<DexterityPower>(
            Owner.Creature,
            DynamicVars[Dexterity].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}