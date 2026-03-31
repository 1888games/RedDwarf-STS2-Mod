
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


public sealed class DefensiveShields() : RedDwarfCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.CAT)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
     
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new BlockVar(6m, ValueProp.Move),
        

    ];

    public override int ScrapRequiredToPlay => 1;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ScrapPower? scrap = Owner.Creature.GetPower<ScrapPower>();

        if (scrap == null) return;

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("KRYTEN: A superlative suggestion, sir, with just two minor flaws. One, we don't have any defensive shields, and two, we don't have any defensive shields. Now I realise that, technically speaking, that's only one flaw but I thought it was such a big one it was worth mentioning twice.", Owner.Creature, 7.5));

        int scrapAmount = scrap.Amount;

        await PowerCmd.ModifyAmount(scrap, -scrapAmount, Owner.Creature, this);

        for (int i = 0; i < scrapAmount; i++)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        }

    }

    protected override void OnUpgrade()
    {

        DynamicVars.Block.UpgradeValueBy(2m);   

    }
}