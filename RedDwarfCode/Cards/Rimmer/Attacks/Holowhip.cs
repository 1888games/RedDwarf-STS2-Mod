using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarf.Code.Cards;


public sealed class Holowhip() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.RIMMER)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new DamageVar(6m, ValueProp.Move)
        
        ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        await CreatureCmd.LoseBlock(cardPlay.Target, cardPlay.Target.Block);

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("Lister to Red Dwarf: We have in our midst a complete smegpot. Brains in the anal region. Chin missing, presumed absent. Genetalia,: small and inoffensive. Of no value or interest.", Owner.Creature, 7.5));


        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext); 
       
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    
    }
}