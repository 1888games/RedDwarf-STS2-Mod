
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class Nanobots() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, RedDwarfCharacter.KRYTEN)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [.. base.CanonicalVars,
        new PowerVar<ShrinkPower>(-1),
     ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ShrinkPower>(),
     
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null) return;

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
      "KRYTEN: You were rebuilt sir, by these itty-bitty, teeny-weeny, tinty little robots!", Owner.Creature, 4));

        await PowerCmd.Apply<ShrinkPower>(cardPlay.Target, DynamicVars["ShrinkPower"].IntValue, Owner.Creature, this);
        
    }

        

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        
   
    }


}