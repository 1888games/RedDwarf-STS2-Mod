using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Cards;


public sealed class BlackCard() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy, RedDwarfCharacter.RIMMER)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new DamageVar(2m, ValueProp.Move)
        
        ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        List<CardModel> cards = PileType.Hand.GetPile(Owner).Cards.ToList();
        cards.AddRange(PileType.Discard.GetPile(Owner).Cards.ToList());
        cards.AddRange(PileType.Exhaust.GetPile(Owner).Cards.ToList());
        cards.AddRange(PileType.Draw.GetPile(Owner).Cards.ToList());

        int count = 0;

        foreach (CardModel c in cards)
        {

            if (c is not RedDwarfCardModel) continue;

            RedDwarfCardModel r = (RedDwarfCardModel)c;

            if (r.Character == Character)
            {
                count++;
            }

        }

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: Da da da, black card, black card, black card, da da da, black card... ", Owner.Creature, 2.5));

        if (IsUpgraded)
        {

            await DamageCmd.Attack(count * DynamicVars.Damage.BaseValue).FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);
            return;

        }

        await DamageCmd.Attack(count * DynamicVars.Damage.BaseValue).FromCard(this)
            .TargetingRandomOpponents(CombatState)
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {
    
    }
}