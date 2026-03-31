
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RedDwarf.Code.Cards;


public sealed class Salvage() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.KRYTEN)
{
    

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new GoldVar(10)
        

    ];

    public override int ScrapRequiredToPlay => 1;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ScrapPower? scrap = Owner.Creature.GetPower<ScrapPower>();

        if (scrap == null) return;

        int scrapAmount = scrap.Amount;

        await PowerCmd.ModifyAmount(scrap, -scrapAmount, Owner.Creature, this);
        await PlayerCmd.GainGold(scrapAmount * DynamicVars.Gold.BaseValue, Owner);
    
    }

    protected override void OnUpgrade()
    {

        DynamicVars.Gold.UpgradeValueBy(5m);   

    }
}