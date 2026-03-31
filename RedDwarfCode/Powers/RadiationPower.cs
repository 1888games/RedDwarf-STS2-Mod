using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HarmonyLib.Code;

namespace RedDwarf.Code.Powers;


public sealed class RadiationPower : RedDwarfPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override Color AmountLabelColor => PowerModel._normalAmountLabelColor;

    private int TriggerCount = 1;

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }

        int iterations = TriggerCount;
        for (int i = 0; i < iterations; i++)
        {
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
            if (base.Owner.IsAlive)
            {
                if (Amount == 1)
                {
                    await PowerCmd.Decrement(this);
                }
                else
                {

                    await PowerCmd.ModifyAmount(this, -Amount / 2, null, null);
                }
                
            }
            else
            {
                await Cmd.CustomScaledWait(0.1f, 0.25f);
            }
        }
    }
}