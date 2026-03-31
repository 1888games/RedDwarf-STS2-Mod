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

public sealed class ItsNotChicken() : RedDwarfCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, RedDwarfCharacter.KRYTEN, true) 
{
 
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SustenancePower>(),
        HoverTipFactory.FromPower<SmegPower>(),
        HoverTipFactory.FromPower<CowedPower>(),

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
      
        new PowerVar<SustenancePower>(7m),
        new PowerVar<SmegPower>(3m),
        new PowerVar<CowedPower>(40m)
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {

        if (CombatState == null) return;

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
           "KRYTEN: That's not chicken, sirs. It's that man we found...", Owner.Creature, 2.5));


        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars[Sustenance].IntValue,
            Owner.Creature,
            this
        );

        await PowerCmd.Apply<CowedPower>(
           Owner.Creature,
           DynamicVars[Cowed].IntValue,
           Owner.Creature,
           this
       );

        await PowerCmd.Apply<SmegPower>(
           base.CombatState.HittableEnemies,
           DynamicVars[Smeg].IntValue,
           Owner.Creature,
           this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(2m);
        DynamicVars[Cowed].UpgradeValueBy(20m);

    }
}