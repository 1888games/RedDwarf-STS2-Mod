using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace RedDwarf.Code.Powers;


public sealed class RegularMealPower : RedDwarfPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override Color AmountLabelColor => PowerModel._normalAmountLabelColor;

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        await PowerCmd.Apply<SustenancePower>(
       Owner,
       (decimal)Amount,
       Owner, null);
    
    }
}