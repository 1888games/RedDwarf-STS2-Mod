using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using RedDwarf.Code.Character;
using RedDwarf.Code.Extensions;
using RedDwarf.Code.Powers;

namespace RedDwarf.Code.Cards;

public enum RedDwarfCharacter { NONE, LISTER, CAT, RIMMER, KRYTEN, ACE, KOCHANSKI };

[Pool(typeof(RedDwarfCardPool))]
public abstract class RedDwarfCardModel :
    CustomCardModel
{
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.

    public RedDwarfCharacter Character = RedDwarfCharacter.NONE;

    public const string Scrap = "ScrapPower";
    public const string Gold = "Gold";
    public const string Sustenance = "SustenancePower";
    public const string SmegDamage = "SmegDamage";
    public const string Smeg = "SmegPower";
    public const string Cowed = "CowedPower";
    public const string Mirror = "MirrorPower";
    public const string Radiation = "RadiationPower";
    public const string Discard = "Discard";
    public const string Confidence = "ConfidencePower";
    public const string Paranoia = "ParanoiaPower";
    public const string SelfDamage = "SelfDamage";
    public const string CardDraw = "CardDraw";
    public const string Draw = "DrawPower";
    public const string Dexterity = "DexterityPower";
    public const string Momentum = "MomentumPower";

    public virtual int ScrapRequiredToPlay => 0;

    public RedDwarfCardModel(

    int canonicalEnergyCost,
    CardType type,
    CardRarity rarity,
    TargetType targetType,
    RedDwarfCharacter character = RedDwarfCharacter.NONE,
    bool shouldShowInCardLibrary = true
   )
    : base(canonicalEnergyCost, type, rarity, targetType)
    {
        Character = character;

        switch (character)
        {
            case RedDwarfCharacter.RIMMER:

                CanonicalVars.AddItem(new PowerVar<RimmerPower>(1m));
                break;

            case RedDwarfCharacter.LISTER:

                CanonicalVars.AddItem(new PowerVar<ListerPower>(1m));
                break;

            case RedDwarfCharacter.CAT:

                CanonicalVars.AddItem(new PowerVar<CatPower>(1m));
                break;

            case RedDwarfCharacter.KOCHANSKI:

                CanonicalVars.AddItem(new PowerVar<KochanskiPower>(1m));
                break;

            case RedDwarfCharacter.KRYTEN:

                CanonicalVars.AddItem(new PowerVar<KrytenPower>(1m));
                break;


            default:
                break;
        }


    }

    protected override bool IsPlayable =>
    base.IsPlayable &&
    (
        ScrapRequiredToPlay == 0 ||
        (Owner.Creature.HasPower<ScrapPower>() &&
         Owner.Creature.GetPower<ScrapPower>().Amount >= ScrapRequiredToPlay)
    );


    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            IHoverTip? hoverTip = Character switch
            {
                RedDwarfCharacter.RIMMER => HoverTipFactory.FromPower<RimmerPower>(),
                RedDwarfCharacter.CAT => HoverTipFactory.FromPower<CatPower>(),
                RedDwarfCharacter.LISTER => HoverTipFactory.FromPower<ListerPower>(),
                RedDwarfCharacter.KRYTEN => HoverTipFactory.FromPower<KrytenPower>(),
                RedDwarfCharacter.KOCHANSKI => HoverTipFactory.FromPower<KochanskiPower>(),

                _ => null
            };


            return hoverTip is null
             ? base.ExtraHoverTips
             : [.. base.ExtraHoverTips, hoverTip];

        }
    }


}