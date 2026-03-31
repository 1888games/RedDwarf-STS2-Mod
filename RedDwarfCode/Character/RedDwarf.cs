using BaseLib.Abstracts;
using RedDwarf.Code.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using RedDwarf.Code.Relics;
using RedDwarf.Code.Cards;


namespace RedDwarf.Code.Character;


public class RedDwarf : PlaceholderCharacterModel
{
    public const string CharacterId = "RedDwarf";
    
    public static readonly Color Color = new("#38743a");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 74;
    
    public override IEnumerable<CardModel> StartingDeck => [

        ModelDb.Card<StrikeRedDwarf>(),
        ModelDb.Card<StrikeRedDwarf>(),
        ModelDb.Card<StrikeRedDwarf>(),
        ModelDb.Card<StrikeRedDwarf>(),
        ModelDb.Card<DefendRedDwarf>(),
        ModelDb.Card<DefendRedDwarf>(),
        ModelDb.Card<DefendRedDwarf>(),
        ModelDb.Card<DefendRedDwarf>(),
        ModelDb.Card<WasteDisposal>(),
        ModelDb.Card<RoofAttack>(),   // 4

        //ModelDb.Card<WasteCompactor>(), 
        //ModelDb.Card<Smeghead>(), 
        //ModelDb.Card<WeldingMallet>(),
        //ModelDb.Card<Triplicator>(),
        //ModelDb.Card<DistendedRectum>(),
        //ModelDb.Card<JusticeField>(),
        //ModelDb.Card<MuttonVindaloo>(),
        //ModelDb.Card<TroutCreme>(),
        //ModelDb.Card<StiffSocks>(),
        //ModelDb.Card<CutToenails>(),
        //ModelDb.Card<ShootingIrons>(),
        //ModelDb.Card<CadmiumTwo>(),
        //ModelDb.Card<WhiteHole>(),
        //ModelDb.Card<DimensionJump>(),
        //ModelDb.Card<ConserveYourEnergy>(),
        //ModelDb.Card<GarbageCannon>(),
        //ModelDb.Card<GarbagePod>(),
        //ModelDb.Card<Scouter>(),
        //ModelDb.Card<RedAlert>(),
        //ModelDb.Card<AutoRepair>(),
        //ModelDb.Card<FriedEgg>(),
        //ModelDb.Card<DefensiveShields>(),
        //ModelDb.Card<Marooned>(),
        //ModelDb.Card<DogFood>(),
        //ModelDb.Card<ConfidenceParanoia>(),
        //ModelDb.Card<Confidence>(),
        //ModelDb.Card<Paranoia>(),
        //ModelDb.Card<TwatIt>(),
        //ModelDb.Card<BatteringRam>(),
        //ModelDb.Card<BarefistFighter>(),
        //ModelDb.Card<Scratch>(),
        //ModelDb.Card<RivieraKid>(),
        //ModelDb.Card<SuperiorReflexes>(),
        //ModelDb.Card<NasalIntuition>(),
        //ModelDb.Card<Catnap>(),
        //ModelDb.Card<StasisBooth>(),
        //ModelDb.Card<ThisIsMine>(),
        //ModelDb.Card<Gimboid>(),
        //ModelDb.Card<LeafletCampaign>(),
        //ModelDb.Card<EverybodysDead>(),
        //ModelDb.Card<TomatoAllergy>(),
        //ModelDb.Card<Lemons>(),
        //ModelDb.Card<Hologram>(),
        //ModelDb.Card<HardLight>(),
        //ModelDb.Card<ChilledSancerre>(),
        //ModelDb.Card<CamphorWoodChest>(),
        //ModelDb.Card<GotMePlan>(),
        //ModelDb.Card<JustProcessing>(),
        //ModelDb.Card<ItsNotChicken>(),
        //ModelDb.Card<PolymorphTwo>(),
        //ModelDb.Card<PolymorphOne>(),
        //ModelDb.Card<FutureEchoes>(),
        //ModelDb.Card<AceRimmer>(),
        //ModelDb.Card<LuckVirus>(),
        //ModelDb.Card<BarefistFighter>(),
        //ModelDb.Card<TexasBookDepository>(),
        //ModelDb.Card<SexualMagnetism>(),
        //ModelDb.Card<Bazookoid>(),
        //ModelDb.Card<GrassyKnoll>(),
        //ModelDb.Card<DoublePolaroid>(),
        //ModelDb.Card<Salvage>(),
        //ModelDb.Card<MeowMissiles>(),
        //ModelDb.Card<MolyDee>(),
        //ModelDb.Card<LimpetMines>(),
        //ModelDb.Card<QueenVictoria>(),
        //ModelDb.Card<Spear>(),
        //ModelDb.Card<BlackCard>(),
        //ModelDb.Card<MakeMyselfBig>(),
        //ModelDb.Card<RadiationLeak>(),
        //ModelDb.Card<Ommmmm>(),
        //ModelDb.Card<DiureticCamels>(),
        //ModelDb.Card<Medicomp>(),
        //ModelDb.Card<DwayneDibley>(),
        //ModelDb.Card<JoySquid>(),
        //ModelDb.Card<DespairSquid>(),
        //ModelDb.Card<Clitoris>(),
     
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<NovaFive>()
    ];
    
    public override CardPoolModel CardPool => ModelDb.CardPool<RedDwarfCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<RedDwarfRelic>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<RedDwarfPotionPool>();
    
    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets. 
        These are just some of the simplest assets, given some placeholders to differentiate your character with. 
        You don't have to, but you're suggested to rename these images. */
    public override string CustomIconTexturePath => "character_icon.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker.png".CharacterUiPath();

    public override string CustomVisualPath => "starbug.tscn".ScenePath();
    public override string CustomCharacterSelectBg => "char_select.tscn".ScenePath();
    public override string CustomIconPath => "icon.tscn".ScenePath();
    // public override string CustomEnergyCounterPath => "energy_counter.tscn".ScenePath();

    public override CustomEnergyCounter? CustomEnergyCounter =>
      new CustomEnergyCounter(EnergyCounterPaths, new Color(0.4f, 0.1f, 0.9f), new Color(0.7f, 0.1f, 0.9f));

    private string EnergyCounterPaths(int i)
    {
        return "res://RedDwarf/images/combat/energy_counters/watcher/watcher_orb_layer_" + i + ".png";
    }

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public override Color EnergyLabelOutlineColor => new("801212FF");

    public override Color DialogueColor => new("590700");

    public override Color MapDrawingColor => new("CB282B");

    public override Color RemoteTargetingLineColor => new("E15847FF");

    public override Color RemoteTargetingLineOutline => new("801212FF");

}