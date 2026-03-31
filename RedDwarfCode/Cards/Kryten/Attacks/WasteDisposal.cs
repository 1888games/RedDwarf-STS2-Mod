
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class WasteDisposal() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.KRYTEN)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
     
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(14m, ValueProp.Move),
        

    ];

    public override int ScrapRequiredToPlay => 1;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        await PowerCmd.ModifyAmount(Owner.Creature.GetPower<ScrapPower>(), -ScrapRequiredToPlay, Owner.Creature, this);



        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
  
        
      

    }

    protected override void OnUpgrade()
    {
        //DynamicVars.Damage.UpgradeValueBy(6m);
        EnergyCost.UpgradeBy(-1);
       

    }
}