using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class FutureEchoes() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.RIMMER, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) { 
   
        CardPlayStartedEntry? cardPlayStartedEntry = CombatManager.Instance.History.CardPlaysStarted.LastOrDefault((CardPlayStartedEntry e) => e.CardPlay.Card.Owner == base.Owner && e.CardPlay.Card != this);
        
        if (cardPlayStartedEntry == null) return;


        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: Ah Holly, is this what you call a future echo?", Owner.Creature, 2.5));


        await CardPileCmd.Add(cardPlayStartedEntry.CardPlay.Card, PileType.Draw, CardPilePosition.Top);
        await CardPileCmd.AutoPlayFromDrawPile(choiceContext, Owner, 1, CardPilePosition.Top, false);
 
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}