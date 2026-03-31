using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Cards;

public sealed class RedAlert() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.KRYTEN, true) 
{

    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [     
        new BlockVar(9m, ValueProp.Move),
        new CardsVar(1)     
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

        if (IsUpgraded)
        {
            CardModel cardModel = (await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this)).FirstOrDefault();
            if (cardModel != null)
            {
                await CardCmd.Discard(choiceContext, cardModel);
            }

            return;
        }

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
    "RIMMER: Step up to red alert!\nKRYTEN: Sir, are you quite sure, it does mean changing the bulb?", Owner.Creature, 4));

        CardPile pile = PileType.Hand.GetPile(base.Owner);
        CardModel cardModel2 = base.Owner.RunState.Rng.CombatCardSelection.NextItem(pile.Cards);
        if (cardModel2 != null)
        {
            await CardCmd.Discard(choiceContext, cardModel2);
        }



    }

    protected override void OnUpgrade()
    {
       
        DynamicVars.Block.UpgradeValueBy(3m);
       // DynamicVars.Cards.UpgradeValueBy(1m);

    }
}