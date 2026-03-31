
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class WasteCompactor() : RedDwarfCardModel(2, CardType.Skill, CardRarity.Rare, TargetType.Self, RedDwarfCharacter.KRYTEN)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
   [
       ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
      
    ];

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new PowerVar<ScrapPower>(1m)

    ];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
       
        List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.ToList();
        int cardCount = list.Count;
        foreach (CardModel item in list)
        {
            await CardCmd.Exhaust(choiceContext, item);
        }

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
    "KRYTEN: I'm almost annoyed!", Owner.Creature, 2.5));

        await PowerCmd.Apply<ScrapPower>(Owner.Creature, cardCount, Owner.Creature, this);

        for (int i = 0; i < cardCount; i++)
        {
            await CardPileCmd.Draw(choiceContext, Owner);
        }
       
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }


}