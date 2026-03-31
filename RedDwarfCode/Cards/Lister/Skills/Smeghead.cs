using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class Smeghead() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
         new PowerVar<CowedPower>(25m),
         new PowerVar<InsultPower>(1m),
         new PowerVar<SmegPower>(1m),

    ];

  
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<CowedPower>(),
        HoverTipFactory.FromPower<InsultPower>(),
        HoverTipFactory.FromPower<SmegPower>(),
       // HoverTipFactory.FromPower<DisgustPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
      
        await PowerCmd.Apply<CowedPower>(
            cardPlay.Target,
            DynamicVars[Cowed].IntValue,
            Owner.Creature,
            this
        );

        await PowerCmd.Apply<SmegPower>(
            cardPlay.Target,
            DynamicVars[Smeg].IntValue,
            Owner.Creature,
            this
        );




    }

    protected override void OnUpgrade()
    {
        DynamicVars[Cowed].UpgradeValueBy(15m);
    }
}