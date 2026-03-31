using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;
using static Godot.OpenXRCompositionLayer;

namespace RedDwarf.Code.Cards;

public sealed class DistendedRectum() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.RIMMER)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
         new PowerVar<CowedPower>(50m),
         new PowerVar<InsultPower>(1m),
        new DynamicVar(CardDraw,1m)

    ];

  
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<CowedPower>(),
         HoverTipFactory.FromPower<InsultPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        // Apply Mark power
        await PowerCmd.Apply<CowedPower>(
            cardPlay.Target,
            DynamicVars[Cowed].IntValue,
            Owner.Creature,
            this
        );

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: STOP YOUR FOUL WHINING, YOU FILTHY PIECE OF DISTENDED RECTUM!", Owner.Creature, 3));

        await CardPileCmd.Draw(choiceContext, DynamicVars[CardDraw].IntValue, Owner);
       
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Cowed].UpgradeValueBy(30m);
    }
}