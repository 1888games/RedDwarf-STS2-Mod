
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace RedDwarf.Code.Cards;

public sealed class ConserveYourEnergy() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.CAT)
{

    protected override bool HasEnergyCostX => true;

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var x = ResolveEnergyXValue();

        if (IsUpgraded)
            x += 1;
        x += 1;


        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("CAT: Look, just conserve your energy. Stan and Ollie will soon be back with supplies. Meanwhile, let's just stay warm and get some sleep.", Owner.Creature, 4));


        await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, x, base.Owner.Creature, this);
        PlayerCmd.EndTurn(Owner, false);

    }

    
}