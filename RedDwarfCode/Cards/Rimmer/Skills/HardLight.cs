using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Cards;

public sealed class HardLight() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self, RedDwarfCharacter.RIMMER, true) 
{
    public override bool GainsBlock => true;


    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
       CardKeyword.Innate,
       CardKeyword.Ethereal
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
       
        new BlockVar(7m, ValueProp.Move),
       
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
            "KRYTEN: Sir, your hard light drive is tougher than vindalood mutton!", Owner.Creature, 3));

        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

      

    }

    protected override void OnUpgrade()
    {

        RemoveKeyword(CardKeyword.Ethereal);
       // DynamicVars.Cards.UpgradeValueBy(1m);

    }
}