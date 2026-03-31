using HarmonyLib;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;


namespace RedDwarf.Code.Cards;

public abstract class CharacterCardDraw(RedDwarfCharacter character) : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self, character)
{
  
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [ ..base.CanonicalVars,
        new DynamicVar(Draw,3)
        
        ];

 
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        CardModel[] drawPile = CardPile.GetCards(base.Owner, PileType.Draw).ToArray();
        int required = DynamicVars[Draw].IntValue;

        List<CardModel> cards = new List<CardModel>();

        foreach(CardModel c in drawPile)
        {
            if (required == 0) break;
            if (c is not RedDwarfCardModel) continue;
            
            RedDwarfCardModel r = (RedDwarfCardModel)c;

            if (r.Character == Character)
            {
                cards.Add(c);
                required--;
            }

        }

        foreach(CardModel c in cards)
        {
            await CardPileCmd.Add(c, PileType.Hand);
        }
  
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Draw].UpgradeValueBy(1);
    }
}