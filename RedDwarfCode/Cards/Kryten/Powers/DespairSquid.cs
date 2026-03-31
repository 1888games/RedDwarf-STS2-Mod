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


public sealed class DespairSquid(): RedDwarfCardModel(1, CardType.Power, CardRarity.Uncommon, TargetType.Self ,RedDwarfCharacter.KRYTEN)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [

        ..base.CanonicalVars,
           new PowerVar<ClitorisPower>(20),


        ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ClitorisPower>(),
        HoverTipFactory.FromPower<CowedPower>(),

    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
            "HOLLY: You had a group hallucination! Brought on by the ink from the Despair Squid.", Owner.Creature, 2.5));
       
        await PowerCmd.Apply<ClitorisPower>(CombatState.HittableEnemies, DynamicVars["ClitorisPower"].BaseValue, Owner.Creature, this);  
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ClitorisPower"].UpgradeValueBy(10);
    }
}
