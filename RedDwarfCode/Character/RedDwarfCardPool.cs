using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using RedDwarf.Code.Cards;
using RedDwarf.Code.Extensions;

namespace RedDwarf.Code.Character;

public class RedDwarfCardPool : CustomCardPoolModel
{

    protected override CardModel[] GenerateAllCards()
    {
        return
        [
            ModelDb.Card<WeldingMallet>(),
            ModelDb.Card<Triplicator>(),
            ModelDb.Card<DistendedRectum>(),
            ModelDb.Card<TakingTheSmeg>(),
            ModelDb.Card<JusticeField>(),
            ModelDb.Card<Smeghead>(),
            ModelDb.Card<MuttonVindaloo>(),
            ModelDb.Card<TroutCreme>(),
            ModelDb.Card<StiffSocks>(),
            ModelDb.Card<CutToenails>(),
            ModelDb.Card<ShootingIrons>(),
            ModelDb.Card<CadmiumTwo>(),
            ModelDb.Card<WhiteHole>(),
            ModelDb.Card<DimensionJump>(),
            ModelDb.Card<ConserveYourEnergy>(),
            ModelDb.Card<GarbageCannon>(),   
            ModelDb.Card<GarbagePod>(),
            ModelDb.Card<Scouter>(),
            ModelDb.Card<RedAlert>(),
            ModelDb.Card<AutoRepair>(),
            ModelDb.Card<FriedEgg>(),
            ModelDb.Card<DefensiveShields>(),
            ModelDb.Card<Marooned>(),
            ModelDb.Card<DogFood>(),
            ModelDb.Card<ConfidenceParanoia>(),
            ModelDb.Card<Confidence>(),
            ModelDb.Card<Paranoia>(),
            ModelDb.Card<TwatIt>(),
            ModelDb.Card<BatteringRam>(),
            ModelDb.Card<BarefistFighter>(),
            ModelDb.Card<Scratch>(),
            ModelDb.Card<RivieraKid>(),
            ModelDb.Card<SuperiorReflexes>(),
            ModelDb.Card<NasalIntuition>(),
            ModelDb.Card<Catnap>(),
            ModelDb.Card<StasisBooth>(),
            ModelDb.Card<Gimboid>(),
            ModelDb.Card<LeafletCampaign>(),
            ModelDb.Card<EverybodysDead>(),
            ModelDb.Card<TomatoAllergy>(),
            ModelDb.Card<Lemons>(),
            ModelDb.Card<WasteCompactor>(),
            ModelDb.Card<Hologram>(),
            ModelDb.Card<HardLight>(),
            ModelDb.Card<ChilledSancerre>(),
            ModelDb.Card<ThisIsMine>(),
            ModelDb.Card<CamphorWoodChest>(),
            ModelDb.Card<GotMePlan>(),
            ModelDb.Card<JustProcessing>(),
            ModelDb.Card<ItsNotChicken>(),
            ModelDb.Card<PolymorphOne>(),
            ModelDb.Card<PolymorphTwo>(),
            ModelDb.Card<FutureEchoes>(),
            ModelDb.Card<AceRimmer>(),
            ModelDb.Card<LuckVirus>(),
            ModelDb.Card<SexualMagnetism>(),
            ModelDb.Card<TexasBookDepository>(),
            ModelDb.Card<Bazookoid>(),
            ModelDb.Card<GrassyKnoll>(),
            ModelDb.Card<DoublePolaroid>(),
            ModelDb.Card<Salvage>(),
            ModelDb.Card<MeowMissiles>(),
            ModelDb.Card<MolyDee>(),
            ModelDb.Card<LimpetMines>(),
            ModelDb.Card<QueenVictoria>(),
            ModelDb.Card<Spear>(),
            ModelDb.Card<BlackCard>(),
            ModelDb.Card<MakeMyselfBig>(),
            ModelDb.Card<RadiationLeak>(),
            ModelDb.Card<Ommmmm>(),
            ModelDb.Card<DiureticCamels>(),
            ModelDb.Card<Nanobots>(),
            ModelDb.Card<Medicomp>(),
            ModelDb.Card<DwayneDibley>(),
            ModelDb.Card<JoySquid>(),
            ModelDb.Card<DespairSquid>(),
            ModelDb.Card<Clitoris>(),
            ModelDb.Card<GroinalExploder>(),
          

        ];

    }

    public override string Title => RedDwarf.CharacterId; //This is not a display name.
    
    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();


    /* These HSV values will determine the color of your card back.
    They are applied as a shader onto an already colored image,
    so it may take some experimentation to find a color you like.
    Generally they should be values between 0 and 1. */
    public override float H => 0f; //Hue; changes the color.
    public override float S => 0.9f; //Saturation
    public override float V => 0.9f; //Brightness
    
    //Alternatively, leave these values at 1 and provide a custom frame image.
    /*public override Texture2D CustomFrame(CustomCardModel card)
    {
        //This will attempt to load CharMod/images/cards/frame.png
        return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
    }*/

    //Color of small card icons
    public override Color DeckEntryCardColor => new("#f03737");
    
    public override bool IsColorless => false;
}