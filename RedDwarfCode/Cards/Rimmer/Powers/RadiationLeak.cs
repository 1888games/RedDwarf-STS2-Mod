using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class RadiationLeak(): RedDwarfCardModel(2, CardType.Power, CardRarity.Uncommon, TargetType.Self ,RedDwarfCharacter.RIMMER)
{
 
    protected override IEnumerable<DynamicVar> CanonicalVars => [

        ..base.CanonicalVars,
        new DynamicVar(Radiation, 4)
   
        ];


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<RadiationPower>(),

    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<RadiationLeakPower>(Owner.Creature, DynamicVars[Radiation].BaseValue, Owner.Creature, this);  
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Radiation].UpgradeValueBy(1);
    }
}
