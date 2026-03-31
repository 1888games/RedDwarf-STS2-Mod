
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace RedDwarf.Code.Cards;

public sealed class StasisBooth() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.LISTER)
{

    protected override bool HasEnergyCostX => true;

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var x = ResolveEnergyXValue();

        if (IsUpgraded)
            x += 1;

        if (x == 0) {

            PlayerCmd.EndTurn(Owner, false);
            return;
        }

        CardModel[] array = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, x), context: choiceContext, player: base.Owner, filter: null, source: this)).ToArray();
        
        foreach (CardModel item in array)
        {
            item.GiveSingleTurnRetain();
        }


        PlayerCmd.EndTurn(Owner, false);


    }

    private bool RetainFilter(CardModel card)
    {
        return !card.ShouldRetainThisTurn;
    }


}