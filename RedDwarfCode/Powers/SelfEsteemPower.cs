

using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace RedDwarf.Code.Powers;

public sealed class SelfEsteemPower : RedDwarfPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
  [
       ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SelfEsteemPower>(),
        HoverTipFactory.FromPower<ConfidencePower>(),
       
    ];

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == base.Owner.Side)
        {
            Flash();
            await PowerCmd.Apply<ConfidencePower>(base.Owner, base.Amount, base.Owner, null);
        }
    }
}
