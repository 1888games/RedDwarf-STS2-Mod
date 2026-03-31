using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class PolymorphOne() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Rare, TargetType.None, RedDwarfCharacter.CAT) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
       
      
    ];

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
         ..base.CanonicalVars,
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CardPoolModel[] pools = ModelDb.AllCharacterCardPools.ToArray();

        List<CardModel> cards = new List<CardModel>();

        foreach(CardPoolModel cpm in pools)
        {
            if (cpm.Title == "RedDwarf") continue;

            cards.AddRange(
                cpm.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
                .Where(p => p.Rarity == CardRarity.Uncommon || p.Rarity == CardRarity.Rare).ToList());

        }

        CardModel? cardModel = CardFactory.GetDistinctForCombat(Owner, from c in cards
                                                                           where c.Type == CardType.Attack
                                                                           select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();
        if (cardModel != null)
        {
            cardModel.SetToFreeThisTurn();
            await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, addedByPlayer: true);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}