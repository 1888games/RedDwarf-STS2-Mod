
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class Marooned() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
     
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new EnergyVar(1)
        

    ];

    public override int ScrapRequiredToPlay => 1;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
        ScrapPower? scrap = Owner.Creature.GetPower<ScrapPower>();

        if (scrap == null ) return;

        int scrapAmount = scrap.Amount;

        await PowerCmd.ModifyAmount(scrap, -scrapAmount, Owner.Creature, this);
        await PlayerCmd.GainEnergy(scrapAmount, base.Owner);

   
    }

    protected override void OnUpgrade()
    {

        AddKeyword(CardKeyword.Retain); 

    }
}