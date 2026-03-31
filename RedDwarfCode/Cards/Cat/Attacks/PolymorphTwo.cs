using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class PolymorphTwo() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, RedDwarfCharacter.CAT) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
       
    ];

   
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
         ..base.CanonicalVars,
          new DamageVar(6m, ValueProp.Move),
          new RepeatVar(1)

    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        if (cardPlay.Target.Monster == null) return;

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).WithHitCount(DynamicVars.Repeat.IntValue).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);


        int currentAmt = DynamicVars.Damage.IntValue;
        int currentRpt = DynamicVars.Repeat.IntValue;

        MonsterModel m = cardPlay.Target.Monster;

        if (m.IntendsToAttack && m.NextMove != null && m.NextMove.Intents != null)
        {
            AttackIntent? intent = (AttackIntent)m.NextMove.Intents.FirstOrDefault(p => p.IntentType == IntentType.Attack);

            if (intent != null)
            {
                DynamicVars.Damage.UpgradeValueBy(
                    intent.GetSingleDamage(new Creature[] { Owner.Creature }, cardPlay.Target) - currentAmt);

                DynamicVars.Repeat.UpgradeValueBy(intent.Repeats - currentRpt);
            }
        }


    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}