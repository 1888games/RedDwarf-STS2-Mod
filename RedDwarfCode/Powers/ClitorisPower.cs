

using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Powers;

public sealed class ClitorisPower : RedDwarfPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
  [
       ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ClitorisPower>(),
        HoverTipFactory.FromPower<CowedPower>(),
       
    ];

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == base.Owner.Side)
        {
            Flash();
            await PowerCmd.Apply<CowedPower>(Owner, Amount, Owner, null);
        }
    }
}
