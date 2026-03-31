using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class MuttonVindaloo() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self, RedDwarfCharacter.LISTER, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SustenancePower>(),
      
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new PowerVar<SustenancePower>(5m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("HOST: Half rice, half chips, and lots more bread and butter to follow!", Owner.Creature, 2.5));

        

        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars[Sustenance].IntValue,
            Owner.Creature,
            this
        );

        await CardPileCmd.Draw(choiceContext, Owner);

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(3m);
    }
}