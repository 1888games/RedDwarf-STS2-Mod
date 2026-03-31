using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class DwayneDibley() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, RedDwarfCharacter.CAT, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>(),
      
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new DamageVar(9m, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)

    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: Just let me check. Thermos, sandwiches, corn plasters, telephone money, dandruff brush, animal footprint chart and... one triple thick condom. You never know!", Owner.Creature, 5));

       
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
           .TargetingAllOpponents(base.CombatState)
           .WithHitFx("vfx/vfx_giant_horizontal_slash")
           .Execute(choiceContext);


    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}