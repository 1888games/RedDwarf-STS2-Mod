using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class GroinalExploder() : RedDwarfCardModel(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(18m, ValueProp.Move),
 
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("LISTER: This thing is gonna propel my love spuds to the far reaches of deep space.", Owner.Creature, 4));

        MonsterModel m = cardPlay.Target.Monster;

        if (m == null) return;

        int count = 1;

        if (m.IntendsToAttack && m.NextMove != null && m.NextMove.Intents != null)
        {
            AttackIntent? intent = (AttackIntent)m.NextMove.Intents.FirstOrDefault(p => p.IntentType == IntentType.Attack);

            if (intent != null)
            {
                count++;
            }
        }

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
          .WithHitCount(count)
          .WithHitFx("vfx/vfx_attack_slash")
          .Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(3m);

    }
}