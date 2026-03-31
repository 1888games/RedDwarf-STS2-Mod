using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Cards;


public sealed class Scratch() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy,RedDwarfCharacter.CAT)
{
 
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [new DamageVar(1m, ValueProp.Move),
        new RepeatVar(4)
        
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");




        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(DynamicVars.Repeat.IntValue).FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Repeat.UpgradeValueBy(1);
    }
}
