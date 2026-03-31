using Godot;
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


public sealed class LimpetMines() : RedDwarfCardModel(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, RedDwarfCharacter.KRYTEN)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(18m, ValueProp.Move),
        new PowerVar<CowedPower>(40m)
    
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
            "HOLLY: There's enough fried calamari out there to feed the whole of Italy!", Owner.Creature, 3.5));

        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), cardPlay.Target, DynamicVars.Damage.BaseValue, ValueProp.Unblockable, Owner.Creature);
        await PowerCmd.Apply<CowedPower>(base.CombatState.HittableEnemies, DynamicVars[Cowed].IntValue, base.Owner.Creature, null);
    }

    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(6m);
        DynamicVars[Cowed].UpgradeValueBy(20m);

    }
}