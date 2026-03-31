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


public sealed class MolyDee() : RedDwarfCardModel(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(8m, ValueProp.Move),
    
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: It's a wibbly gun! It makes things go all wibblified!", Owner.Creature, 2.5));

        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), cardPlay.Target, DynamicVars.Damage.BaseValue, ValueProp.Unblockable, Owner.Creature);

    }

    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(4m);

    }
}