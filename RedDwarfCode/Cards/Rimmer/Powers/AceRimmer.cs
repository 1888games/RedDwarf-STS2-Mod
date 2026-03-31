using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class AceRimmer(): RedDwarfCardModel(1, CardType.Power, CardRarity.Uncommon, TargetType.Self ,RedDwarfCharacter.RIMMER)
{
 
    protected override IEnumerable<DynamicVar> CanonicalVars => [

        ..base.CanonicalVars,
        new DynamicVar(Confidence, 2)
   
        ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>(),
        HoverTipFactory.FromPower<SelfEsteemPower>(),
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("ACE: Smoke me a kipper...I'll be be back for breakfast!", Owner.Creature, 2.5));
       
        await PowerCmd.Apply<SelfEsteemPower>(Owner.Creature, DynamicVars[Confidence].BaseValue, Owner.Creature, this);  
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
