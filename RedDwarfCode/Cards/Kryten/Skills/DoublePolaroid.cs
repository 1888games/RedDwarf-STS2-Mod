
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;
using System.Drawing;

namespace RedDwarf.Code.Cards;

public sealed class DoublePolaroid() : RedDwarfCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.KRYTEN)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [.. base.CanonicalVars,
        new PowerVar<ConfidencePower>(6),
        new PowerVar<ParanoiaPower>(1),
     ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ConfidencePower>(),
         HoverTipFactory.FromPower<ParanoiaPower>()
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
        "LISTER: Kryte, I don’t care what model it was! No vacuum cleaner should give a human being a double polaroid!", Owner.Creature, 4));

        await PowerCmd.Apply<ConfidencePower>(Owner.Creature, DynamicVars[Confidence].IntValue, Owner.Creature, this);
        await PowerCmd.Apply<ParanoiaPower>(CombatState.HittableEnemies, DynamicVars[Paranoia].IntValue, Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
   
    }


}