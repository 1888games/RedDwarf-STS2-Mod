using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;


namespace RedDwarf.Code.Powers;

public sealed class MakeMyselfBigPower : RedDwarfPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

 
    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power.Owner != Owner) return;

        if (power is not ScrapPower) return;

        if (amount >= 0) return;

        await PowerCmd.Apply<DexterityPower>(Owner, -amount, Owner, null, false);

    }


    
}