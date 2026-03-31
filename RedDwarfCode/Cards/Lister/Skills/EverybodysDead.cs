using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public sealed class EverybodysDead() : RedDwarfCardModel(2, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies, RedDwarfCharacter.LISTER) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<RadiationPower>(),
      
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
         ..base.CanonicalVars,
        new PowerVar<RadiationPower>(16m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState == null) return;
     
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create(
        "HOLLY: They're dead, Dave. Everybody Dave. Everybody's dead, Dave. They're all dead, everybody's dead, Dave. Everybody's dead, everybody is deead, Dave.", Owner.Creature, 6));


        foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
        {
            NCreature nCreature = NCombatRoom.Instance?.GetCreatureNode(hittableEnemy);
            if (nCreature != null)
            {
                NGaseousImpactVfx child = NGaseousImpactVfx.Create(nCreature.VfxSpawnPosition, new Color("83eb85"));
                NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(child);
            }
        }

        await PowerCmd.Apply<RadiationPower>(base.CombatState.HittableEnemies, DynamicVars[Radiation].BaseValue, base.Owner.Creature, null);


    }

    protected override void OnUpgrade()
    {
        DynamicVars[Radiation].UpgradeValueBy(4m);
    }
}