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

public sealed class ChilledSancerre() : RedDwarfCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.RIMMER, true) 
{
    public override bool GainsBlock => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SustenancePower>(),
       
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
      
        new PowerVar<SustenancePower>(7m),
        new BlockVar(12m, ValueProp.Move)
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        //  NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("The name's Dangerous Dan McGrew - barefist fighter extraordinaire.", Owner.Creature, 2.5));

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("RIMMER: And when you've killed them, burn the bodies, then bring me the cold ashes on a silver plate, with a glass of chilled Sancerre.", Owner.Creature, 5));

        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars[Sustenance].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(2m);
        DynamicVars.Block.UpgradeValueBy(3m);

    }
}