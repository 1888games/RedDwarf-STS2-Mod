
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Powers;

public sealed class ParanoiaPower : RedDwarfPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        
        if (base.Owner.IsPlayer && side == CombatSide.Player)
        {
            ConfidencePower? confidence = Owner.GetPower<ConfidencePower>();

            if (confidence == null || confidence.Amount == 0)
            {
                await PowerCmd.Remove(this);
                return;
            }


            Flash();

            for (int i = 0; i < Amount; i++)
            {

                await PowerCmd.Decrement(confidence);

                if (confidence.Amount == 0)
                {
                    await PowerCmd.Remove(this);
                    return;
                }
            }

            return;

        }
        
        if (base.Owner.IsEnemy && side == CombatSide.Enemy)
        {
            await PowerCmd.Apply<ConfidencePower>(Owner, -Amount, Owner, null);
        }
        
    }

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer == base.Owner && base.Owner.IsEnemy && result.UnblockedDamage > 0)
        {
            await PowerCmd.Remove(this);

            ConfidencePower? confidence = Owner.GetPower<ConfidencePower>();
            if (confidence != null && confidence.Amount < 0) await PowerCmd.Remove(confidence);

        }
    }
}
