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

public sealed class TroutCreme() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self, RedDwarfCharacter.CAT, true) 
{
    public override bool GainsBlock => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SustenancePower>(),
       
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
      
        new PowerVar<SustenancePower>(5m),
        new BlockVar(4m, ValueProp.Move)
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: FISH!\nMACHINE: Today's fish is Trout à la Crème, enjoy your meal!\nCAT: FISH!\nMACHINE: Today's fish is Trout à la Crème, enjoy your meal!\nCAT: FISH!\nMACHINE: Today's fish is Trout à la Crème, enjoy your meal!", Owner.Creature, 5));


        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars[Sustenance].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(3m);
        DynamicVars.Block.UpgradeValueBy(2m);

    }
}