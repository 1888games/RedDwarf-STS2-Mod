using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class Medicomp() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Common, TargetType.Self, RedDwarfCharacter.KRYTEN)
{

    public override HashSet<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust
        ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
   [
       ..base.CanonicalVars,
         new DynamicVar(Cowed, 4)

   ];



    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
      
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars[Cowed].BaseValue);
       
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}