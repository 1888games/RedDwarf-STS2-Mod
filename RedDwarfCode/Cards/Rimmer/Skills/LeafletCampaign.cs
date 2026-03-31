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
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class LeafletCampaign() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies, RedDwarfCharacter.RIMMER)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
   [
       ..base.CanonicalVars,
         new PowerVar<CowedPower>(50m),
         new PowerVar<InsultPower>(1m),
       
   ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<CowedPower>(),
         HoverTipFactory.FromPower<InsultPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (CombatState == null) return;

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: Hit them hard and fast with a major, and I mean major, leaflet campaign.", Owner.Creature, 3));


        await PowerCmd.Apply<CowedPower>(base.CombatState.HittableEnemies, DynamicVars[Cowed].IntValue, base.Owner.Creature, null);

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Cowed].UpgradeValueBy(25m);
    }
}