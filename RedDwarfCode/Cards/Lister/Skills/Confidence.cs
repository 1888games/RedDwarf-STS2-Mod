
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class Confidence() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.LISTER)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [.. base.CanonicalVars,
        new PowerVar<ConfidencePower>(2),
      
     ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>()
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
    "CONFIDENCE: Hey! It's the king! Mr. Beautiful!", Owner.Creature, 3));

        await PowerCmd.Apply<ConfidencePower>(Owner.Creature, DynamicVars[Confidence].IntValue, Owner.Creature, this);
      
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Confidence].UpgradeValueBy(1);
   
    }


}