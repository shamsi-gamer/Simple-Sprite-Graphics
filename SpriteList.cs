using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class Texture
        {
            public string Tag;
            public string ID;
            public float  Width;
            public float  Height;


            public Texture(string tag, string id, float width, float height)
            {
                Tag    = tag;
                ID     = id;
                Width  = width;
                Height = height;
            }


            public static Texture From(string tag)
            {
                return Array.Find(SpriteTextures, t => t.Tag == tag.ToUpper());
            }
        }


        public static Texture SquareTexture, CircleTexture;

        public static readonly Texture[] SpriteTextures = 
        {
                             new Texture("HBS",   "AH_BoreSight",	                                                     64,   64),
                             new Texture("HGN",   "AH_GravityHudNegativeDegrees",                                      300,   31),
                             new Texture("HGP",   "AH_GravityHudPositiveDegrees",	                                    300,   31),
                             new Texture("HPU",   "AH_PullUp",                                                         100,  100),
                             new Texture("HTB",   "AH_TextBox",                                                         80,   31),
                             new Texture("HVV",   "AH_VelocityVector",                                                  51,   51),
                            
                             new Texture("AR",    "Arrow",                                                             512,  512),
            CircleTexture  = new Texture("CRC",   "Circle",                                                            512,  512),
                             new Texture("CRH",   "CircleHollow",                                                      512,  512),
                             new Texture("CNS",   "Construction",                                                      512,  512),
                             new Texture("CRS",   "Cross",                                                             512,  512),
                             new Texture("DNG",   "Danger",                                                            512,  512),
                             new Texture("DBL",   "DecorativeBracketLeft",                                             128,  512),
                             new Texture("DBR",   "DecorativeBracketRight",                                            128,  512),
                             new Texture("GRD",   "Grid",                                                             2048, 2048),
                                                  
                             new Texture("IE",    "IconEnergy",                                                        128,  128),
                             new Texture("IH",    "IconHydrogen",                                                      128,  128),
                             new Texture("IO",    "IconOxygen",                                                        128,  128),
                             
                             //new SpriteTexture("",    "IconTemperature",                                                   ?,    ?),

                             new Texture("PEBG",  "LCD_Economy_Badge",                                                1024, 1024),
                             new Texture("PEB2",  "LCD_Economy_Blueprint_2",                                          1024, 1024),
                             new Texture("PEB3",  "LCD_Economy_Blueprint_3",                                          1024, 1024),
                             new Texture("PECH",  "LCD_Economy_Charts",                                               1024, 1024),
                             new Texture("PECL",  "LCD_Economy_Clear",                                                1024, 1024),
                             new Texture("PECO",  "LCD_Economy_Coins",                                                1024, 1024),
                             new Texture("PED",   "LCD_Economy_Detail",                                                512,   64),
                             new Texture("PEF1",  "LCD_Economy_Faction_1",                                            1024, 1024),
                             new Texture("PEG",   "LCD_Economy_Graph",                                                1024, 1024),
                             new Texture("PEG2",  "LCD_Economy_Graph_2",                                              1024, 1024),
                             new Texture("PEG3",  "LCD_Economy_Graph_3",                                              1024, 1024),
                             new Texture("PEG4",  "LCD_Economy_Graph_4",                                              1024, 1024),
                             new Texture("PEG5",  "LCD_Economy_Graph_5",                                              1024, 1024),
                             new Texture("PEKS",  "LCD_Economy_KeenSWH",                                              1024, 1024),
                             new Texture("PEP1",  "LCD_Economy_Poster_1",                                             1024, 1024),
                             new Texture("PEBP",  "LCD_Economy_SC_Blueprint",                                         1024, 1024),
                             new Texture("PEH",   "LCD_Economy_SC_Here",                                              1024, 1024),
                             new Texture("PEL",   "LCD_Economy_SC_Logo",                                              1024, 1024),
                             new Texture("PEL2",  "LCD_Economy_SC_Logo_2",                                            1024, 1024),
                             new Texture("PEL3",  "LCD_Economy_SE_Logo_1",                                            1024, 1024),
                             new Texture("PEL4",  "LCD_Economy_SE_Logo_2",                                            1024, 1024),
                             new Texture("PESC",  "LCD_Economy_SingleCoin",                                           1024, 1024),
                             new Texture("PETR",  "LCD_Economy_Trade",                                                1024, 1024),
                             new Texture("PETN",  "LCD_Economy_Trinity",                                              1024, 1024),
                             new Texture("PEV",   "LCD_Economy_Vending_Bg",                                           2048, 1024),
                             new Texture("PF1",   "LCD_Frozen_Poster01",                                              1024, 1024),
                             new Texture("PF2",   "LCD_Frozen_Poster02",                                              1024, 1024),
                             new Texture("PF3",   "LCD_Frozen_Poster03",                                              1024, 1024),
                             new Texture("PF4",   "LCD_Frozen_Poster04",                                              1024, 1024),
                             new Texture("PF5",   "LCD_Frozen_Poster05",                                              1024, 1024),
                             new Texture("PF6",   "LCD_Frozen_Poster06",                                              1024, 1024),
                             new Texture("PF7",   "LCD_Frozen_Poster07",                                              1024, 1024),
                             new Texture("P1L",   "LCD_HI_Poster1_Landscape",                                         1920, 1200),
                             new Texture("P1P",   "LCD_HI_Poster1_Portrait",                                          1200, 1920),
                             new Texture("P1S",   "LCD_HI_Poster1_Square",                                            2048, 2048),
                             new Texture("P2L",   "LCD_HI_Poster2_Landscape",                                         1920, 1200),
                             new Texture("P2P",   "LCD_HI_Poster2_Portrait",                                          1200, 1920),
                             new Texture("P2S",   "LCD_HI_Poster2_Square",                                            2048, 2048),
                             new Texture("P3L",   "LCD_HI_Poster3_Landscape",                                         1920, 1200),
                             new Texture("P3P",   "LCD_HI_Poster3_Portrait",                                          1200, 1920),
                             new Texture("P3S",   "LCD_HI_Poster3_Square",                                            2048, 2048),
                             new Texture("PBL",   "LCD_SoF_BrightFuture_Landscape",                                   2048, 1024),
                             new Texture("PBP",   "LCD_SoF_BrightFuture_Portrait",                                    1024, 2048),
                             new Texture("PBS",   "LCD_SoF_BrightFuture_Square",                                      2048, 2048),
                             new Texture("PCL",   "LCD_SoF_CosmicTeam_Landscape",                                     2048, 1024),
                             new Texture("PCP",   "LCD_SoF_CosmicTeam_Portrait",                                      1024, 2048),
                             new Texture("PCS",   "LCD_SoF_CosmicTeam_Square",                                        2048, 2048),
                             new Texture("PEL",   "LCD_SoF_Exploration_Landscape",                                    2048, 1024),
                             new Texture("PEP",   "LCD_SoF_Exploration_Portrait",                                     1024, 2048),
                             new Texture("PES",   "LCD_SoF_Exploration_Square",                                       2048, 2048),
                             new Texture("PSL",   "LCD_SoF_SpaceTravel_Landscape",                                    2048, 1024),
                             new Texture("PSP",   "LCD_SoF_SpaceTravel_Portrait",                                     1024, 2048),
                             new Texture("PSS",   "LCD_SoF_SpaceTravel_Square",                                       2048, 2048),
                             new Texture("PTL",   "LCD_SoF_ThunderFleet_Landscape",                                   2048, 1024),
                             new Texture("PTP",   "LCD_SoF_ThunderFleet_Portrait",                                    1024, 2048),
                             new Texture("PTS",   "LCD_SoF_ThunderFleet_Square",                                      2048, 2048),
                                                  
                             new Texture("ACC",   "MyObjectBuilder_AmmoMagazine/AutocannonClip",                       128,  128),
                             new Texture("AA",    "MyObjectBuilder_AmmoMagazine/AutomaticRifleGun_Mag_20rd",           128,  128),
                             new Texture("AE",    "MyObjectBuilder_AmmoMagazine/ElitePistolMagazine",                  128,  128),
                             new Texture("AF",    "MyObjectBuilder_AmmoMagazine/FullAutoPistolMagazine",               128,  128),
                             new Texture("ALC",   "MyObjectBuilder_AmmoMagazine/LargeCalibreAmmo",                     128,  128),
                             new Texture("AMC",   "MyObjectBuilder_AmmoMagazine/MediumCalibreAmmo",                    128,  128),
                             new Texture("ALR",   "MyObjectBuilder_AmmoMagazine/LargeRailgunAmmo",                     128,  128),
                             new Texture("ASR",   "MyObjectBuilder_AmmoMagazine/SmallRailgunAmmo",                     128,  128),
                             new Texture("AM",    "MyObjectBuilder_AmmoMagazine/Missile200mm",                         128,  128),
                             new Texture("AN2",   "MyObjectBuilder_AmmoMagazine/NATO_25x184mm",                        128,  128),
                             new Texture("AN5",   "MyObjectBuilder_AmmoMagazine/NATO_5p56x45mm",                       128,  128),
                             new Texture("APA",   "MyObjectBuilder_AmmoMagazine/PreciseAutomaticRifleGun_Mag_5rd",     128,  128),
                             new Texture("ARA",   "MyObjectBuilder_AmmoMagazine/RapidFireAutomaticRifleGun_Mag_50rd",  128,  128),
                             new Texture("AS",    "MyObjectBuilder_AmmoMagazine/SemiAutoPistolMagazine",               128,  128),
                             new Texture("AUA",   "MyObjectBuilder_AmmoMagazine/UltimateAutomaticRifleGun_Mag_30rd",   128,  128),
                                                  
                             new Texture("CBP",   "MyObjectBuilder_Component/BulletproofGlass",                        128,  128),
                             new Texture("CC",    "MyObjectBuilder_Component/Canvas",                                  128,  128),
                             new Texture("CMP",   "MyObjectBuilder_Component/Computer",                                128,  128),
                             new Texture("CST",   "MyObjectBuilder_Component/Construction",                            128,  128),
                             new Texture("CDT",   "MyObjectBuilder_Component/Detector",                                128,  128),
                             new Texture("CDS",   "MyObjectBuilder_Component/Display",                                 128,  128),
                             new Texture("CEP",   "MyObjectBuilder_Component/EngineerPlushie",                         128,  128),
                             new Texture("CEX",   "MyObjectBuilder_Component/Explosives",                              128,  128),
                             new Texture("CGR",   "MyObjectBuilder_Component/Girder",                                  128,  128),
                             new Texture("CGG",   "MyObjectBuilder_Component/GravityGenerator",                        128,  128),
                             new Texture("CIP",   "MyObjectBuilder_Component/InteriorPlate",                           128,  128),
                             new Texture("CLT",   "MyObjectBuilder_Component/LargeTube",                               128,  128),
                             new Texture("CMD",   "MyObjectBuilder_Component/Medical",                                 128,  128),
                             new Texture("CMG",   "MyObjectBuilder_Component/MetalGrid",                               128,  128),
                             new Texture("CMT",   "MyObjectBuilder_Component/Motor",                                   128,  128),
                             new Texture("CPC",   "MyObjectBuilder_Component/PowerCell",                               128,  128),
                             new Texture("CRC",   "MyObjectBuilder_Component/RadioCommunication",                      128,  128),
                             new Texture("CR",    "MyObjectBuilder_Component/Reactor",                                 128,  128),
                             new Texture("CST",   "MyObjectBuilder_Component/SmallTube",                               128,  128),
                             new Texture("CSC",   "MyObjectBuilder_Component/SolarCell",                               128,  128),
                             new Texture("CSP",   "MyObjectBuilder_Component/SteelPlate",                              128,  128),
                             new Texture("CS",    "MyObjectBuilder_Component/Superconductor",                          128,  128),
                             new Texture("CT",    "MyObjectBuilder_Component/Thrust",                                  128,  128),
                             new Texture("CZC",   "MyObjectBuilder_Component/ZoneChip",                                128,  128),
                             new Texture("CCL",   "MyObjectBuilder_ConsumableItem/ClangCola",                          128,  128),
                             new Texture("CCC",   "MyObjectBuilder_ConsumableItem/CosmicCoffee",                       128,  128),
                             new Texture("CMK",   "MyObjectBuilder_ConsumableItem/Medkit",                             128,  128),
                             new Texture("СPK",   "MyObjectBuilder_ConsumableItem/Powerkit",                           128,  128),
                             new Texture("CDP",   "MyObjectBuilder_Datapad/Datapad",                                   128,  128),
                             new Texture("CHB",   "MyObjectBuilder_GasContainerObject/HydrogenBottle",                 128,  128),
                                                  
                             new Texture("ICB",   "MyObjectBuilder_Ingot/Cobalt",                                      128,  128),
                             new Texture("IAU",   "MyObjectBuilder_Ingot/Gold",                                        128,  128),
                             new Texture("IFE",   "MyObjectBuilder_Ingot/Iron",                                        128,  128),
                             new Texture("IMG",   "MyObjectBuilder_Ingot/Magnesium",                                   128,  128),
                             new Texture("INI",   "MyObjectBuilder_Ingot/Nickel",                                      128,  128),
                             new Texture("IPL",   "MyObjectBuilder_Ingot/Platinum",                                    128,  128),
                             new Texture("ISC",   "MyObjectBuilder_Ingot/Scrap",                                       128,  128),
                             new Texture("ISI",   "MyObjectBuilder_Ingot/Silicon",                                     128,  128),
                             new Texture("IAG",   "MyObjectBuilder_Ingot/Silver",                                      128,  128),
                             new Texture("IST",   "MyObjectBuilder_Ingot/Stone",                                       128,  128),
                             new Texture("IUR",   "MyObjectBuilder_Ingot/Uranium",                                     128,  128),
                                                  
                             new Texture("OCO",   "MyObjectBuilder_Ore/Cobalt",                                        128,  128),
                             new Texture("OAU",   "MyObjectBuilder_Ore/Gold",                                          128,  128),
                             new Texture("OH2",   "MyObjectBuilder_Ore/Ice",                                           128,  128),
                             new Texture("OFE",   "MyObjectBuilder_Ore/Iron",                                          128,  128),
                             new Texture("OMG",   "MyObjectBuilder_Ore/Magnesium",                                     128,  128),
                             new Texture("ONI",   "MyObjectBuilder_Ore/Nickel",                                        128,  128),
                             new Texture("OOR",   "MyObjectBuilder_Ore/Organic",                                       128,  128),
                             new Texture("OPL",   "MyObjectBuilder_Ore/Platinum",                                      128,  128),
                             new Texture("OSC",   "MyObjectBuilder_Ore/Scrap",                                         128,  128),
                             new Texture("OSI",   "MyObjectBuilder_Ore/Silicon",                                       128,  128),
                             new Texture("OAG",   "MyObjectBuilder_Ore/Silver",                                        128,  128),
                             new Texture("OST",   "MyObjectBuilder_Ore/Stone",                                         128,  128),
                             new Texture("OUR",   "MyObjectBuilder_Ore/Uranium",                                       128,  128),
                             
                             new Texture("OXB",   "MyObjectBuilder_OxygenContainerObject/OxygenBottle",                128,  128),
                                                                                                                       
                             new Texture("TP",    "MyObjectBuilder_Package/Package",                                   128,  128),
                             new Texture("TAL",   "MyObjectBuilder_PhysicalGunObject/AdvancedHandHeldLauncherItem",    128,  128),
                             new Texture("TG",    "MyObjectBuilder_PhysicalGunObject/AngleGrinderItem",                128,  128),
                             new Texture("TG2",   "MyObjectBuilder_PhysicalGunObject/AngleGrinder2Item",               128,  128),
                             new Texture("TG3",   "MyObjectBuilder_PhysicalGunObject/AngleGrinder3Item",               128,  128),
                             new Texture("TG4",   "MyObjectBuilder_PhysicalGunObject/AngleGrinder4Item",               128,  128),
                             new Texture("TAR",   "MyObjectBuilder_PhysicalGunObject/AutomaticRifleItem",              128,  128),
                             new Texture("TBL",   "MyObjectBuilder_PhysicalGunObject/BasicHandHeldLauncherItem",       128,  128),
                             new Texture("TEP",   "MyObjectBuilder_PhysicalGunObject/ElitePistolItem",                 128,  128),
                             new Texture("TSP",   "MyObjectBuilder_PhysicalGunObject/SemiAutoPistolItem",              128,  128),
                             new Texture("TAP",   "MyObjectBuilder_PhysicalGunObject/FullAutoPistolItem",              128,  128),
                             new Texture("TRP",   "MyObjectBuilder_PhysicalGunObject/GoodAIRewardPunishmentTool",      128,  128),
                             new Texture("TD",    "MyObjectBuilder_PhysicalGunObject/HandDrillItem",                   128,  128),
                             new Texture("TD2",   "MyObjectBuilder_PhysicalGunObject/HandDrill2Item",                  128,  128),
                             new Texture("TD3",   "MyObjectBuilder_PhysicalGunObject/HandDrill3Item",                  128,  128),
                             new Texture("TD4",   "MyObjectBuilder_PhysicalGunObject/HandDrill4Item",                  128,  128),
                             new Texture("TPR",   "MyObjectBuilder_PhysicalGunObject/PreciseAutomaticRifleItem",       128,  128),
                             new Texture("TRR",   "MyObjectBuilder_PhysicalGunObject/RapidFireAutomaticRifleItem",     128,  128),
                             new Texture("TUR",   "MyObjectBuilder_PhysicalGunObject/UltimateAutomaticRifleItem",      128,  128),
                             new Texture("TW",    "MyObjectBuilder_PhysicalGunObject/WelderItem",                      128,  128),
                             new Texture("TW2",   "MyObjectBuilder_PhysicalGunObject/Welder2Item",                     128,  128),
                             new Texture("TW3",   "MyObjectBuilder_PhysicalGunObject/Welder3Item",                     128,  128),
                             new Texture("TW4",   "MyObjectBuilder_PhysicalGunObject/Welder4Item",                     128,  128),
                             new Texture("SC",    "MyObjectBuilder_PhysicalObject/SpaceCredit",                        128,  128),
                                                                                                                       
                             new Texture("TDB",   "MyObjectBuilder_TreeObject/DeadBushMedium",                         128,  128),
                             new Texture("TDBM",  "MyObjectBuilder_TreeObject/DesertBushMedium",                       128,  128),
                             new Texture("TDT",   "MyObjectBuilder_TreeObject/DesertTree",                             128,  128),
                             new Texture("TDTD",  "MyObjectBuilder_TreeObject/DesertTreeDead",                         128,  128),
                             new Texture("TDTDM", "MyObjectBuilder_TreeObject/DesertTreeDeadMedium",                   128,  128),
                             new Texture("TDTM",  "MyObjectBuilder_TreeObject/DesertTreeMedium",                       128,  128),
                             new Texture("TLB1",  "MyObjectBuilder_TreeObject/LeafBushMedium_var1",                    128,  128),
                             new Texture("TLB2",  "MyObjectBuilder_TreeObject/LeafBushMedium_var2",                    128,  128),
                             new Texture("TLT",   "MyObjectBuilder_TreeObject/LeafTree",                               128,  128),
                             new Texture("TLTM",  "MyObjectBuilder_TreeObject/LeafTreeMedium",                         128,  128),
                             new Texture("TPBM",  "MyObjectBuilder_TreeObject/PineBushMedium",                         128,  128),
                             new Texture("TPT",   "MyObjectBuilder_TreeObject/PineTree",                               128,  128),
                             new Texture("TPTM",  "MyObjectBuilder_TreeObject/PineTreeMedium",                         128,  128),
                             new Texture("TPTS",  "MyObjectBuilder_TreeObject/PineTreeSnow",                           128,  128),
                             new Texture("TPTSM", "MyObjectBuilder_TreeObject/PineTreeSnowMedium",                     128,  128),
                             new Texture("TSPBM", "MyObjectBuilder_TreeObject/SnowPineBushMedium",                     128,  128),
                                                                                                                       
                             new Texture("NE",    "No Entry",                                                          512,  512),
                             new Texture("OFF",   "Offline",                                                           512,  512),
                             new Texture("OFW",   "Offline_wide",                                                      512,  128),
                             new Texture("ON",    "Online",                                                            512,  512),
                             new Texture("ONW",   "Online_wide",                                                       512,  128),
                             //new SirpteTexture("",    "OutOfOrder",                                                        ?,    ?),
                             new Texture("TRI",   "RightTriangle",                                                     512,  512),
                             new Texture("SLB",   "Screen_LoadingBar",                                                 256,  256),
                             new Texture("SLB2",  "Screen_LoadingBar2",                                                256,  256),
                             new Texture("SCR",   "SemiCircle",                                                        512,  512),
                             new Texture("SQH",   "SquareHollow",                                                      512,  512),
            SquareTexture  = new Texture("SQR",   "SquareSimple",                                                        4,    4),
                             new Texture("SQT",   "SquareTapered",                                                      32,   32),
                             new Texture("STB2",  "StoreBlock2",                                                      1024, 1024),
                             
                             new Texture("B1",    "Textures\\FactionLogo\\Builders\\BuilderIcon_1.dds",                256,  256),
                             new Texture("B10",   "Textures\\FactionLogo\\Builders\\BuilderIcon_10.dds",               256,  256),
                             new Texture("B11",   "Textures\\FactionLogo\\Builders\\BuilderIcon_11.dds",               256,  256),
                             new Texture("B12",   "Textures\\FactionLogo\\Builders\\BuilderIcon_12.dds",               256,  256),
                             new Texture("B13",   "Textures\\FactionLogo\\Builders\\BuilderIcon_13.dds",               256,  256),
                             new Texture("B14",   "Textures\\FactionLogo\\Builders\\BuilderIcon_14.dds",               256,  256),
                             new Texture("B15",   "Textures\\FactionLogo\\Builders\\BuilderIcon_15.dds",               256,  256),
                             new Texture("B16",   "Textures\\FactionLogo\\Builders\\BuilderIcon_16.dds",               256,  256),
                             new Texture("B2",    "Textures\\FactionLogo\\Builders\\BuilderIcon_2.dds",                256,  256),
                             new Texture("B3",    "Textures\\FactionLogo\\Builders\\BuilderIcon_3.dds",                256,  256),
                             new Texture("B4",    "Textures\\FactionLogo\\Builders\\BuilderIcon_4.dds",                256,  256),
                             new Texture("B5",    "Textures\\FactionLogo\\Builders\\BuilderIcon_5.dds",                256,  256),
                             new Texture("B6",    "Textures\\FactionLogo\\Builders\\BuilderIcon_6.dds",                256,  256),
                             new Texture("B7",    "Textures\\FactionLogo\\Builders\\BuilderIcon_7.dds",                256,  256),
                             new Texture("B8",    "Textures\\FactionLogo\\Builders\\BuilderIcon_8.dds",                256,  256),
                             new Texture("B9",    "Textures\\FactionLogo\\Builders\\BuilderIcon_9.dds",                256,  256),
                             new Texture("EMP",   "Textures\\FactionLogo\\Empty.dds",                                  256,  256),
                             new Texture("M1",    "Textures\\FactionLogo\\Miners\\MinerIcon_1.dds",                    256,  256),
                             new Texture("M2",    "Textures\\FactionLogo\\Miners\\MinerIcon_2.dds",                    256,  256),
                             new Texture("M3",    "Textures\\FactionLogo\\Miners\\MinerIcon_3.dds",                    256,  256),
                             new Texture("M4",    "Textures\\FactionLogo\\Miners\\MinerIcon_4.dds",                    256,  256),
                             new Texture("O1",    "Textures\\FactionLogo\\Others\\OtherIcon_1.dds",                    256,  256),
                             new Texture("O10",   "Textures\\FactionLogo\\Others\\OtherIcon_10.dds",                   256,  256),
                             new Texture("O11",   "Textures\\FactionLogo\\Others\\OtherIcon_11.dds",                   256,  256),
                             new Texture("O12",   "Textures\\FactionLogo\\Others\\OtherIcon_12.dds",                   256,  256),
                             new Texture("O13",   "Textures\\FactionLogo\\Others\\OtherIcon_13.dds",                   256,  256),
                             new Texture("O14",   "Textures\\FactionLogo\\Others\\OtherIcon_14.dds",                   256,  256),
                             new Texture("O15",   "Textures\\FactionLogo\\Others\\OtherIcon_15.dds",                   256,  256),
                             new Texture("O16",   "Textures\\FactionLogo\\Others\\OtherIcon_16.dds",                   256,  256),
                             new Texture("O17",   "Textures\\FactionLogo\\Others\\OtherIcon_17.dds",                   256,  256),
                             new Texture("O18",   "Textures\\FactionLogo\\Others\\OtherIcon_18.dds",                   256,  256),
                             new Texture("O19",   "Textures\\FactionLogo\\Others\\OtherIcon_19.dds",                   256,  256),
                             new Texture("O2",    "Textures\\FactionLogo\\Others\\OtherIcon_2.dds",                    256,  256),
                             new Texture("O20",   "Textures\\FactionLogo\\Others\\OtherIcon_20.dds",                   256,  256),
                             new Texture("O21",   "Textures\\FactionLogo\\Others\\OtherIcon_21.dds",                   256,  256),
                             new Texture("O22",   "Textures\\FactionLogo\\Others\\OtherIcon_22.dds",                   256,  256),
                             new Texture("O23",   "Textures\\FactionLogo\\Others\\OtherIcon_23.dds",                   256,  256),
                             new Texture("O24",   "Textures\\FactionLogo\\Others\\OtherIcon_24.dds",                   256,  256),
                             new Texture("O26",   "Textures\\FactionLogo\\Others\\OtherIcon_26.dds",                   256,  256),
                             new Texture("O27",   "Textures\\FactionLogo\\Others\\OtherIcon_27.dds",                   256,  256),
                             new Texture("O28",   "Textures\\FactionLogo\\Others\\OtherIcon_28.dds",                   256,  256),
                             new Texture("O29",   "Textures\\FactionLogo\\Others\\OtherIcon_29.dds",                   256,  256),
                             new Texture("O3",    "Textures\\FactionLogo\\Others\\OtherIcon_3.dds",                    256,  256),
                             new Texture("O30",   "Textures\\FactionLogo\\Others\\OtherIcon_30.dds",                   256,  256),
                             new Texture("O31",   "Textures\\FactionLogo\\Others\\OtherIcon_31.dds",                   256,  256),
                             new Texture("O32",   "Textures\\FactionLogo\\Others\\OtherIcon_32.dds",                   256,  256),
                             new Texture("O33",   "Textures\\FactionLogo\\Others\\OtherIcon_33.dds",                   256,  256),
                             new Texture("O4",    "Textures\\FactionLogo\\Others\\OtherIcon_4.dds",                    256,  256),
                             new Texture("O5",    "Textures\\FactionLogo\\Others\\OtherIcon_5.dds",                    256,  256),
                             new Texture("O6",    "Textures\\FactionLogo\\Others\\OtherIcon_6.dds",                    256,  256),
                             new Texture("O7",    "Textures\\FactionLogo\\Others\\OtherIcon_7.dds",                    256,  256),
                             new Texture("O8",    "Textures\\FactionLogo\\Others\\OtherIcon_8.dds",                    256,  256),
                             new Texture("O9",    "Textures\\FactionLogo\\Others\\OtherIcon_9.dds",                    256,  256),
                             new Texture("ICP",   "Textures\\FactionLogo\\PirateIcon.dds",                             256,  256),
                             new Texture("SPD",   "Textures\\FactionLogo\\Spiders.dds",                                256,  256),
                             new Texture("ICT1",  "Textures\\FactionLogo\\Traders\\TraderIcon_1.dds",                  256,  256),
                             new Texture("ICT2",  "Textures\\FactionLogo\\Traders\\TraderIcon_2.dds",                  256,  256),
                             new Texture("ICT3",  "Textures\\FactionLogo\\Traders\\TraderIcon_3.dds",                  256,  256),
                             new Texture("ICT4",  "Textures\\FactionLogo\\Traders\\TraderIcon_4.dds",                  256,  256),
                             new Texture("ICT5",  "Textures\\FactionLogo\\Traders\\TraderIcon_5.dds",                  256,  256),
                             new Texture("TRI",   "Triangle",                                                          512,  512),
                             new Texture("UVC",   "UVChecker",                                                        1024, 1024),
                             new Texture("WS",    "White screen",                                                        4,    4)
        };
    }
}
