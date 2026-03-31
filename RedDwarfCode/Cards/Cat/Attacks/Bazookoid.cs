using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class Bazookoid() : RedDwarfCardModel(2, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy, RedDwarfCharacter.CAT)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<WeakPower>(),
  

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(13m, ValueProp.Move),
        new PowerVar<WeakPower>(1m),
        new PowerVar<CowedPower>(4m)

    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Creature enemy = base.Owner.RunState.Rng.CombatTargets.NextItem(base.CombatState.HittableEnemies);

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(enemy)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        var otherEnemies = base.CombatState.HittableEnemies
     .  Where(e => e != enemy);

        foreach(Creature c in otherEnemies)
        {
            await DamageCmd.Attack(DynamicVars[Cowed].BaseValue).FromCard(this).Targeting(c)
           .WithHitFx("vfx/vfx_attack_slash")
           .Execute(choiceContext);

        }

        await PowerCmd.Apply<WeakPower>(
            enemy,
            DynamicVars.Weak.IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4m);
        DynamicVars.Weak.UpgradeValueBy(1m);
        DynamicVars[Cowed].UpgradeValueBy(2m);

    }
}