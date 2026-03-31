
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

public sealed class Ommmmm() : RedDwarfCardModel(2, CardType.Power, CardRarity.Rare, TargetType.Self, RedDwarfCharacter.LISTER)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [.. base.CanonicalVars,
        new PowerVar<MomentumPower>(1),
 

     ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<MomentumPower>(),
    
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: Keep writing those hits, kid!", Owner.Creature, 3));


        await PowerCmd.Apply<MomentumPower>(Owner.Creature, DynamicVars[Momentum].IntValue, Owner.Creature, this); 
    }

    protected override void OnUpgrade()
    {

        EnergyCost.UpgradeBy(-1);
    }


}