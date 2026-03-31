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


public sealed class TexasBookDepository() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.KRYTEN)
{
    const int damage1 = 3;
    const int damage2 = 8;
    const int damage3 = 20;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new PowerVar<CowedPower>(damage1),
        new PowerVar<SmegPower>(damage2),
        new PowerVar<RadiationPower>(damage3),
    
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

      
        int damageIndex = base.Owner.RunState.Rng.MonsterAi.NextInt(3);
        int damageBase = DynamicVars[Smeg].IntValue;

        string speech = "Sixth Floor...";

        switch (damageIndex)
        {
            case 1:
                damageBase = DynamicVars[Smeg].IntValue;
                speech = "Fifth Floor, low damage!";
                break;
            case 2:
                damageBase = DynamicVars[Radiation].IntValue;
                speech = "Sixth Floor, high damage!";
                break;
            case 0:
                damageBase = DynamicVars[Cowed].IntValue;
                speech = "Fourth Floor, medium damage!";
                break;
            default:
                break;
        }

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(speech, Owner.Creature, 2.5));


        await DamageCmd.Attack(damageBase).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {

        DynamicVars[Cowed].UpgradeValueBy(2);
        DynamicVars[Smeg].UpgradeValueBy(4);
        DynamicVars[Radiation].UpgradeValueBy(7);

    }
}