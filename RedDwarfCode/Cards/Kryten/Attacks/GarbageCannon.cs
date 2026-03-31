
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class GarbageCannon() : RedDwarfCardModel(2, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy, RedDwarfCharacter.KRYTEN)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
     
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(6m, ValueProp.Move),
        

    ];

    public override int ScrapRequiredToPlay => 1;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
        ScrapPower? scrap = Owner.Creature.GetPower<ScrapPower>();

        if (scrap == null || CombatState == null) return;

        int scrapAmount = scrap.Amount;

        await PowerCmd.ModifyAmount(scrap, -scrapAmount, Owner.Creature, this);

        if (IsUpgraded)
        {

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(scrapAmount).FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);
            return;

        }

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(scrapAmount).FromCard(this)
            .TargetingRandomOpponents(CombatState)
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);


    }

    protected override void OnUpgrade()
    {
       
     
     //   DynamicVars.Damage.UpgradeValueBy(1m);
      

    }
}