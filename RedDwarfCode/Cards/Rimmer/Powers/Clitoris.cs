using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class Clitoris(): RedDwarfCardModel(2, CardType.Power, CardRarity.Uncommon, TargetType.Self ,RedDwarfCharacter.RIMMER)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [

        ..base.CanonicalVars,
           new PowerVar<WeakPower>(99)

        ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<WeakPower>(),


    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: The Committee for the Liberation and Integration of Terrifying Organisms and their Rehabilitation Into Society.", Owner.Creature, 5));
       
        await PowerCmd.Apply<WeakPower>(CombatState.HittableEnemies, DynamicVars.Weak.BaseValue, Owner.Creature, this);  
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
