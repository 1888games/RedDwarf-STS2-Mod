using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using RedDwarf.Code.Powers;


namespace RedDwarf.Code.Relics;

public sealed class SsTrojan: RedDwarfRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;


    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        var nova5 = this;
        if (side != nova5.Owner.Creature.Side)
            return;

        ScrapPower power = await PowerCmd.Apply<ScrapPower>(Owner.Creature, 1, Owner.Creature, null);

        nova5.Flash();
    }
}