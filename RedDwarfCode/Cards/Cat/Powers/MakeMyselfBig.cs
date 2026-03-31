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

public sealed class MakeMyselfBig() : RedDwarfCardModel(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.CAT, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<DexterityPower>(),
      
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new PowerVar<DexterityPower>(1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: Uh oh! Better make myself look big!", Owner.Creature, 2.5));

        await PowerCmd.Apply<MakeMyselfBigPower>(
            Owner.Creature,
            DynamicVars[Dexterity].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}