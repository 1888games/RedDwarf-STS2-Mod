using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;


public sealed class MeowMissiles() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, RedDwarfCharacter.CAT)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(7m, ValueProp.Move),
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var cards = PileType.Hand.GetPile(Owner).Cards;

        int count = 0;

        foreach (CardModel c in cards) { 

            if (c is not RedDwarfCardModel) continue;

            RedDwarfCardModel r = (RedDwarfCardModel)c;

            if (r.Character == Character)
            {
                count++;
            }

        }

        NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("MEEEEOWW! BOOOOOM! MEEEEEOW! CABOOM!", Owner.Creature, 2.5));


        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(count).FromCard(this)
           .TargetingAllOpponents(base.CombatState)
           .WithHitFx("vfx/vfx_giant_horizontal_slash")
           .Execute(choiceContext);

    }


 
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);

    }
}