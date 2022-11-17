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
        public class SpriteTexture
        {
            public string Tag;
            public string ID;
            public float  Width;
            public float  Height;


            public SpriteTexture(string tag, string id, float width, float height)
            {
                Tag    = tag;
                ID     = id;
                Width  = width;
                Height = height;
            }


            public static SpriteTexture From(string tag)
            {
                return Array.Find(SpriteTextures, t => t.Tag == tag.ToUpper());
            }
        }


        public static readonly SpriteTexture[] SpriteTextures = 
        {
            new SpriteTexture("HBS",   "AH_BoreSight",	                                                     64,   64),
            new SpriteTexture("HGN",   "AH_GravityHudNegativeDegrees",                                      300,   31),
            new SpriteTexture("HGP",   "AH_GravityHudPositiveDegrees",	                                    300,   31),
            new SpriteTexture("HPU",   "AH_PullUp",                                                         100,  100),
            new SpriteTexture("HTB",   "AH_TextBox",                                                         80,   31),
            new SpriteTexture("HVV",   "AH_VelocityVector",                                                  51,   51),

            new SpriteTexture("AR",    "Arrow",                                                             512,  512),
            new SpriteTexture("CRC",   "Circle",                                                            512,  512),
            new SpriteTexture("CRH",   "CircleHollow",                                                      512,  512),
            new SpriteTexture("CNS",   "Construction",                                                      512,  512),
            new SpriteTexture("CRS",   "Cross",                                                             512,  512),
            new SpriteTexture("DNG",   "Danger",                                                            512,  512),
            new SpriteTexture("DBL",   "DecorativeBracketLeft",                                             128,  512),
            new SpriteTexture("DBR",   "DecorativeBracketRight",                                            128,  512),
            new SpriteTexture("GRD",   "Grid",                                                             2048, 2048),
                                 
            new SpriteTexture("IE",    "IconEnergy",                                                        128,  128),
            new SpriteTexture("IH",    "IconHydrogen",                                                      128,  128),
            new SpriteTexture("IO",    "IconOxygen",                                                        128,  128),
            
            //new Texture("",    "IconTemperature",                                                   ?,    ?),

            new SpriteTexture("PEBG",  "LCD_Economy_Badge",                                                1024, 1024),
            new SpriteTexture("PEB2",  "LCD_Economy_Blueprint_2",                                          1024, 1024),
            new SpriteTexture("PEB3",  "LCD_Economy_Blueprint_3",                                          1024, 1024),
            new SpriteTexture("PECH",  "LCD_Economy_Charts",                                               1024, 1024),
            new SpriteTexture("PECL",  "LCD_Economy_Clear",                                                1024, 1024),
            new SpriteTexture("PECO",  "LCD_Economy_Coins",                                                1024, 1024),
            new SpriteTexture("PED",   "LCD_Economy_Detail",                                                512,   64),
            new SpriteTexture("PEF1",  "LCD_Economy_Faction_1",                                            1024, 1024),
            new SpriteTexture("PEG",   "LCD_Economy_Graph",                                                1024, 1024),
            new SpriteTexture("PEG2",  "LCD_Economy_Graph_2",                                              1024, 1024),
            new SpriteTexture("PEG3",  "LCD_Economy_Graph_3",                                              1024, 1024),
            new SpriteTexture("PEG4",  "LCD_Economy_Graph_4",                                              1024, 1024),
            new SpriteTexture("PEG5",  "LCD_Economy_Graph_5",                                              1024, 1024),
            new SpriteTexture("PEKS",  "LCD_Economy_KeenSWH",                                              1024, 1024),
            new SpriteTexture("PEP1",  "LCD_Economy_Poster_1",                                             1024, 1024),
            new SpriteTexture("PEBP",  "LCD_Economy_SC_Blueprint",                                         1024, 1024),
            new SpriteTexture("PEH",   "LCD_Economy_SC_Here",                                              1024, 1024),
            new SpriteTexture("PEL",   "LCD_Economy_SC_Logo",                                              1024, 1024),
            new SpriteTexture("PEL2",  "LCD_Economy_SC_Logo_2",                                            1024, 1024),
            new SpriteTexture("PEL3",  "LCD_Economy_SE_Logo_1",                                            1024, 1024),
            new SpriteTexture("PEL4",  "LCD_Economy_SE_Logo_2",                                            1024, 1024),
            new SpriteTexture("PESC",  "LCD_Economy_SingleCoin",                                           1024, 1024),
            new SpriteTexture("PETR",  "LCD_Economy_Trade",                                                1024, 1024),
            new SpriteTexture("PETN",  "LCD_Economy_Trinity",                                              1024, 1024),
            new SpriteTexture("PEV",   "LCD_Economy_Vending_Bg",                                           2048, 1024),
            new SpriteTexture("PF1",   "LCD_Frozen_Poster01",                                              1024, 1024),
            new SpriteTexture("PF2",   "LCD_Frozen_Poster02",                                              1024, 1024),
            new SpriteTexture("PF3",   "LCD_Frozen_Poster03",                                              1024, 1024),
            new SpriteTexture("PF4",   "LCD_Frozen_Poster04",                                              1024, 1024),
            new SpriteTexture("PF5",   "LCD_Frozen_Poster05",                                              1024, 1024),
            new SpriteTexture("PF6",   "LCD_Frozen_Poster06",                                              1024, 1024),
            new SpriteTexture("PF7",   "LCD_Frozen_Poster07",                                              1024, 1024),
            new SpriteTexture("P1L",   "LCD_HI_Poster1_Landscape",                                         1920, 1200),
            new SpriteTexture("P1P",   "LCD_HI_Poster1_Portrait",                                          1200, 1920),
            new SpriteTexture("P1S",   "LCD_HI_Poster1_Square",                                            2048, 2048),
            new SpriteTexture("P2L",   "LCD_HI_Poster2_Landscape",                                         1920, 1200),
            new SpriteTexture("P2P",   "LCD_HI_Poster2_Portrait",                                          1200, 1920),
            new SpriteTexture("P2S",   "LCD_HI_Poster2_Square",                                            2048, 2048),
            new SpriteTexture("P3L",   "LCD_HI_Poster3_Landscape",                                         1920, 1200),
            new SpriteTexture("P3P",   "LCD_HI_Poster3_Portrait",                                          1200, 1920),
            new SpriteTexture("P3S",   "LCD_HI_Poster3_Square",                                            2048, 2048),
            new SpriteTexture("PBL",   "LCD_SoF_BrightFuture_Landscape",                                   2048, 1024),
            new SpriteTexture("PBP",   "LCD_SoF_BrightFuture_Portrait",                                    1024, 2048),
            new SpriteTexture("PBS",   "LCD_SoF_BrightFuture_Square",                                      2048, 2048),
            new SpriteTexture("PCL",   "LCD_SoF_CosmicTeam_Landscape",                                     2048, 1024),
            new SpriteTexture("PCP",   "LCD_SoF_CosmicTeam_Portrait",                                      1024, 2048),
            new SpriteTexture("PCS",   "LCD_SoF_CosmicTeam_Square",                                        2048, 2048),
            new SpriteTexture("PEL",   "LCD_SoF_Exploration_Landscape",                                    2048, 1024),
            new SpriteTexture("PEP",   "LCD_SoF_Exploration_Portrait",                                     1024, 2048),
            new SpriteTexture("PES",   "LCD_SoF_Exploration_Square",                                       2048, 2048),
            new SpriteTexture("PSL",   "LCD_SoF_SpaceTravel_Landscape",                                    2048, 1024),
            new SpriteTexture("PSP",   "LCD_SoF_SpaceTravel_Portrait",                                     1024, 2048),
            new SpriteTexture("PSS",   "LCD_SoF_SpaceTravel_Square",                                       2048, 2048),
            new SpriteTexture("PTL",   "LCD_SoF_ThunderFleet_Landscape",                                   2048, 1024),
            new SpriteTexture("PTP",   "LCD_SoF_ThunderFleet_Portrait",                                    1024, 2048),
            new SpriteTexture("PTS",   "LCD_SoF_ThunderFleet_Square",                                      2048, 2048),
                                 
            new SpriteTexture("ACC",   "MyObjectBuilder_AmmoMagazine/AutocannonClip",                       128,  128),
            new SpriteTexture("AA",    "MyObjectBuilder_AmmoMagazine/AutomaticRifleGun_Mag_20rd",           128,  128),
            new SpriteTexture("AE",    "MyObjectBuilder_AmmoMagazine/ElitePistolMagazine",                  128,  128),
            new SpriteTexture("AF",    "MyObjectBuilder_AmmoMagazine/FullAutoPistolMagazine",               128,  128),
            new SpriteTexture("ALC",   "MyObjectBuilder_AmmoMagazine/LargeCalibreAmmo",                     128,  128),
            new SpriteTexture("AMC",   "MyObjectBuilder_AmmoMagazine/MediumCalibreAmmo",                    128,  128),
            new SpriteTexture("ALR",   "MyObjectBuilder_AmmoMagazine/LargeRailgunAmmo",                     128,  128),
            new SpriteTexture("ASR",   "MyObjectBuilder_AmmoMagazine/SmallRailgunAmmo",                     128,  128),
            new SpriteTexture("AM",    "MyObjectBuilder_AmmoMagazine/Missile200mm",                         128,  128),
            new SpriteTexture("AN2",   "MyObjectBuilder_AmmoMagazine/NATO_25x184mm",                        128,  128),
            new SpriteTexture("AN5",   "MyObjectBuilder_AmmoMagazine/NATO_5p56x45mm",                       128,  128),
            new SpriteTexture("APA",   "MyObjectBuilder_AmmoMagazine/PreciseAutomaticRifleGun_Mag_5rd",     128,  128),
            new SpriteTexture("ARA",   "MyObjectBuilder_AmmoMagazine/RapidFireAutomaticRifleGun_Mag_50rd",  128,  128),
            new SpriteTexture("AS",    "MyObjectBuilder_AmmoMagazine/SemiAutoPistolMagazine",               128,  128),
            new SpriteTexture("AUA",   "MyObjectBuilder_AmmoMagazine/UltimateAutomaticRifleGun_Mag_30rd",   128,  128),
                                 
            new SpriteTexture("CBP",   "MyObjectBuilder_Component/BulletproofGlass",                        128,  128),
            new SpriteTexture("CC",    "MyObjectBuilder_Component/Canvas",                                  128,  128),
            new SpriteTexture("CMP",   "MyObjectBuilder_Component/Computer",                                128,  128),
            new SpriteTexture("CST",   "MyObjectBuilder_Component/Construction",                            128,  128),
            new SpriteTexture("CDT",   "MyObjectBuilder_Component/Detector",                                128,  128),
            new SpriteTexture("CDS",   "MyObjectBuilder_Component/Display",                                 128,  128),
            new SpriteTexture("CEP",   "MyObjectBuilder_Component/EngineerPlushie",                         128,  128),
            new SpriteTexture("CEX",   "MyObjectBuilder_Component/Explosives",                              128,  128),
            new SpriteTexture("CGR",   "MyObjectBuilder_Component/Girder",                                  128,  128),
            new SpriteTexture("CGG",   "MyObjectBuilder_Component/GravityGenerator",                        128,  128),
            new SpriteTexture("CIP",   "MyObjectBuilder_Component/InteriorPlate",                           128,  128),
            new SpriteTexture("CLT",   "MyObjectBuilder_Component/LargeTube",                               128,  128),
            new SpriteTexture("CMD",   "MyObjectBuilder_Component/Medical",                                 128,  128),
            new SpriteTexture("CMG",   "MyObjectBuilder_Component/MetalGrid",                               128,  128),
            new SpriteTexture("CMT",   "MyObjectBuilder_Component/Motor",                                   128,  128),
            new SpriteTexture("CPC",   "MyObjectBuilder_Component/PowerCell",                               128,  128),
            new SpriteTexture("CRC",   "MyObjectBuilder_Component/RadioCommunication",                      128,  128),
            new SpriteTexture("CR",    "MyObjectBuilder_Component/Reactor",                                 128,  128),
            new SpriteTexture("CST",   "MyObjectBuilder_Component/SmallTube",                               128,  128),
            new SpriteTexture("CSC",   "MyObjectBuilder_Component/SolarCell",                               128,  128),
            new SpriteTexture("CSP",   "MyObjectBuilder_Component/SteelPlate",                              128,  128),
            new SpriteTexture("CS",    "MyObjectBuilder_Component/Superconductor",                          128,  128),
            new SpriteTexture("CT",    "MyObjectBuilder_Component/Thrust",                                  128,  128),
            new SpriteTexture("CZC",   "MyObjectBuilder_Component/ZoneChip",                                128,  128),
            new SpriteTexture("CCL",   "MyObjectBuilder_ConsumableItem/ClangCola",                          128,  128),
            new SpriteTexture("CCC",   "MyObjectBuilder_ConsumableItem/CosmicCoffee",                       128,  128),
            new SpriteTexture("CMK",   "MyObjectBuilder_ConsumableItem/Medkit",                             128,  128),
            new SpriteTexture("СPK",   "MyObjectBuilder_ConsumableItem/Powerkit",                           128,  128),
            new SpriteTexture("CDP",   "MyObjectBuilder_Datapad/Datapad",                                   128,  128),
            new SpriteTexture("CHB",   "MyObjectBuilder_GasContainerObject/HydrogenBottle",                 128,  128),
                                 
            new SpriteTexture("ICB",   "MyObjectBuilder_Ingot/Cobalt",                                      128,  128),
            new SpriteTexture("IAU",   "MyObjectBuilder_Ingot/Gold",                                        128,  128),
            new SpriteTexture("IFE",   "MyObjectBuilder_Ingot/Iron",                                        128,  128),
            new SpriteTexture("IMG",   "MyObjectBuilder_Ingot/Magnesium",                                   128,  128),
            new SpriteTexture("INI",   "MyObjectBuilder_Ingot/Nickel",                                      128,  128),
            new SpriteTexture("IPL",   "MyObjectBuilder_Ingot/Platinum",                                    128,  128),
            new SpriteTexture("ISC",   "MyObjectBuilder_Ingot/Scrap",                                       128,  128),
            new SpriteTexture("ISI",   "MyObjectBuilder_Ingot/Silicon",                                     128,  128),
            new SpriteTexture("IAG",   "MyObjectBuilder_Ingot/Silver",                                      128,  128),
            new SpriteTexture("IST",   "MyObjectBuilder_Ingot/Stone",                                       128,  128),
            new SpriteTexture("IUR",   "MyObjectBuilder_Ingot/Uranium",                                     128,  128),
                                 
            new SpriteTexture("OCO",   "MyObjectBuilder_Ore/Cobalt",                                        128,  128),
            new SpriteTexture("OAU",   "MyObjectBuilder_Ore/Gold",                                          128,  128),
            new SpriteTexture("OH2",   "MyObjectBuilder_Ore/Ice",                                           128,  128),
            new SpriteTexture("OFE",   "MyObjectBuilder_Ore/Iron",                                          128,  128),
            new SpriteTexture("OMG",   "MyObjectBuilder_Ore/Magnesium",                                     128,  128),
            new SpriteTexture("ONI",   "MyObjectBuilder_Ore/Nickel",                                        128,  128),
            new SpriteTexture("OOR",   "MyObjectBuilder_Ore/Organic",                                       128,  128),
            new SpriteTexture("OPL",   "MyObjectBuilder_Ore/Platinum",                                      128,  128),
            new SpriteTexture("OSC",   "MyObjectBuilder_Ore/Scrap",                                         128,  128),
            new SpriteTexture("OSI",   "MyObjectBuilder_Ore/Silicon",                                       128,  128),
            new SpriteTexture("OAG",   "MyObjectBuilder_Ore/Silver",                                        128,  128),
            new SpriteTexture("OST",   "MyObjectBuilder_Ore/Stone",                                         128,  128),
            new SpriteTexture("OUR",   "MyObjectBuilder_Ore/Uranium",                                       128,  128),
            
            new SpriteTexture("OXB",   "MyObjectBuilder_OxygenContainerObject/OxygenBottle",                128,  128),
                                                                                                      
            new SpriteTexture("TP",    "MyObjectBuilder_Package/Package",                                   128,  128),
            new SpriteTexture("TAL",   "MyObjectBuilder_PhysicalGunObject/AdvancedHandHeldLauncherItem",    128,  128),
            new SpriteTexture("TG",    "MyObjectBuilder_PhysicalGunObject/AngleGrinderItem",                128,  128),
            new SpriteTexture("TG2",   "MyObjectBuilder_PhysicalGunObject/AngleGrinder2Item",               128,  128),
            new SpriteTexture("TG3",   "MyObjectBuilder_PhysicalGunObject/AngleGrinder3Item",               128,  128),
            new SpriteTexture("TG4",   "MyObjectBuilder_PhysicalGunObject/AngleGrinder4Item",               128,  128),
            new SpriteTexture("TAR",   "MyObjectBuilder_PhysicalGunObject/AutomaticRifleItem",              128,  128),
            new SpriteTexture("TBL",   "MyObjectBuilder_PhysicalGunObject/BasicHandHeldLauncherItem",       128,  128),
            new SpriteTexture("TEP",   "MyObjectBuilder_PhysicalGunObject/ElitePistolItem",                 128,  128),
            new SpriteTexture("TSP",   "MyObjectBuilder_PhysicalGunObject/SemiAutoPistolItem",              128,  128),
            new SpriteTexture("TAP",   "MyObjectBuilder_PhysicalGunObject/FullAutoPistolItem",              128,  128),
            new SpriteTexture("TRP",   "MyObjectBuilder_PhysicalGunObject/GoodAIRewardPunishmentTool",      128,  128),
            new SpriteTexture("TD",    "MyObjectBuilder_PhysicalGunObject/HandDrillItem",                   128,  128),
            new SpriteTexture("TD2",   "MyObjectBuilder_PhysicalGunObject/HandDrill2Item",                  128,  128),
            new SpriteTexture("TD3",   "MyObjectBuilder_PhysicalGunObject/HandDrill3Item",                  128,  128),
            new SpriteTexture("TD4",   "MyObjectBuilder_PhysicalGunObject/HandDrill4Item",                  128,  128),
            new SpriteTexture("TPR",   "MyObjectBuilder_PhysicalGunObject/PreciseAutomaticRifleItem",       128,  128),
            new SpriteTexture("TRR",   "MyObjectBuilder_PhysicalGunObject/RapidFireAutomaticRifleItem",     128,  128),
            new SpriteTexture("TUR",   "MyObjectBuilder_PhysicalGunObject/UltimateAutomaticRifleItem",      128,  128),
            new SpriteTexture("TW",    "MyObjectBuilder_PhysicalGunObject/WelderItem",                      128,  128),
            new SpriteTexture("TW2",   "MyObjectBuilder_PhysicalGunObject/Welder2Item",                     128,  128),
            new SpriteTexture("TW3",   "MyObjectBuilder_PhysicalGunObject/Welder3Item",                     128,  128),
            new SpriteTexture("TW4",   "MyObjectBuilder_PhysicalGunObject/Welder4Item",                     128,  128),
            new SpriteTexture("SC",    "MyObjectBuilder_PhysicalObject/SpaceCredit",                        128,  128),
                                                                                                      
            new SpriteTexture("TDB",   "MyObjectBuilder_TreeObject/DeadBushMedium",                         128,  128),
            new SpriteTexture("TDBM",  "MyObjectBuilder_TreeObject/DesertBushMedium",                       128,  128),
            new SpriteTexture("TDT",   "MyObjectBuilder_TreeObject/DesertTree",                             128,  128),
            new SpriteTexture("TDTD",  "MyObjectBuilder_TreeObject/DesertTreeDead",                         128,  128),
            new SpriteTexture("TDTDM", "MyObjectBuilder_TreeObject/DesertTreeDeadMedium",                   128,  128),
            new SpriteTexture("TDTM",  "MyObjectBuilder_TreeObject/DesertTreeMedium",                       128,  128),
            new SpriteTexture("TLB1",  "MyObjectBuilder_TreeObject/LeafBushMedium_var1",                    128,  128),
            new SpriteTexture("TLB2",  "MyObjectBuilder_TreeObject/LeafBushMedium_var2",                    128,  128),
            new SpriteTexture("TLT",   "MyObjectBuilder_TreeObject/LeafTree",                               128,  128),
            new SpriteTexture("TLTM",  "MyObjectBuilder_TreeObject/LeafTreeMedium",                         128,  128),
            new SpriteTexture("TPBM",  "MyObjectBuilder_TreeObject/PineBushMedium",                         128,  128),
            new SpriteTexture("TPT",   "MyObjectBuilder_TreeObject/PineTree",                               128,  128),
            new SpriteTexture("TPTM",  "MyObjectBuilder_TreeObject/PineTreeMedium",                         128,  128),
            new SpriteTexture("TPTS",  "MyObjectBuilder_TreeObject/PineTreeSnow",                           128,  128),
            new SpriteTexture("TPTSM", "MyObjectBuilder_TreeObject/PineTreeSnowMedium",                     128,  128),
            new SpriteTexture("TSPBM", "MyObjectBuilder_TreeObject/SnowPineBushMedium",                     128,  128),
                                                                                                      
            new SpriteTexture("NE",    "No Entry",                                                          512,  512),
            new SpriteTexture("OFF",   "Offline",                                                           512,  512),
            new SpriteTexture("OFW",   "Offline_wide",                                                      512,  128),
            new SpriteTexture("ON",    "Online",                                                            512,  512),
            new SpriteTexture("ONW",   "Online_wide",                                                       512,  128),
            //new Texture("",    "OutOfOrder",                                                        ?,    ?),
            new SpriteTexture("TRI",   "RightTriangle",                                                     512,  512),
            new SpriteTexture("SLB",   "Screen_LoadingBar",                                                 256,  256),
            new SpriteTexture("SLB2",  "Screen_LoadingBar2",                                                256,  256),
            new SpriteTexture("SCR",   "SemiCircle",                                                        512,  512),
            new SpriteTexture("SQH",   "SquareHollow",                                                      512,  512),
            new SpriteTexture("SQR",   "SquareSimple",                                                        4,    4),
            new SpriteTexture("SQT",   "SquareTapered",                                                      32,   32),
            new SpriteTexture("STB2",  "StoreBlock2",                                                      1024, 1024),

            new SpriteTexture("B1",    "Textures\\FactionLogo\\Builders\\BuilderIcon_1.dds",                256,  256),
            new SpriteTexture("B10",   "Textures\\FactionLogo\\Builders\\BuilderIcon_10.dds",               256,  256),
            new SpriteTexture("B11",   "Textures\\FactionLogo\\Builders\\BuilderIcon_11.dds",               256,  256),
            new SpriteTexture("B12",   "Textures\\FactionLogo\\Builders\\BuilderIcon_12.dds",               256,  256),
            new SpriteTexture("B13",   "Textures\\FactionLogo\\Builders\\BuilderIcon_13.dds",               256,  256),
            new SpriteTexture("B14",   "Textures\\FactionLogo\\Builders\\BuilderIcon_14.dds",               256,  256),
            new SpriteTexture("B15",   "Textures\\FactionLogo\\Builders\\BuilderIcon_15.dds",               256,  256),
            new SpriteTexture("B16",   "Textures\\FactionLogo\\Builders\\BuilderIcon_16.dds",               256,  256),
            new SpriteTexture("B2",    "Textures\\FactionLogo\\Builders\\BuilderIcon_2.dds",                256,  256),
            new SpriteTexture("B3",    "Textures\\FactionLogo\\Builders\\BuilderIcon_3.dds",                256,  256),
            new SpriteTexture("B4",    "Textures\\FactionLogo\\Builders\\BuilderIcon_4.dds",                256,  256),
            new SpriteTexture("B5",    "Textures\\FactionLogo\\Builders\\BuilderIcon_5.dds",                256,  256),
            new SpriteTexture("B6",    "Textures\\FactionLogo\\Builders\\BuilderIcon_6.dds",                256,  256),
            new SpriteTexture("B7",    "Textures\\FactionLogo\\Builders\\BuilderIcon_7.dds",                256,  256),
            new SpriteTexture("B8",    "Textures\\FactionLogo\\Builders\\BuilderIcon_8.dds",                256,  256),
            new SpriteTexture("B9",    "Textures\\FactionLogo\\Builders\\BuilderIcon_9.dds",                256,  256),
            new SpriteTexture("EMP",   "Textures\\FactionLogo\\Empty.dds",                                  256,  256),
            new SpriteTexture("M1",    "Textures\\FactionLogo\\Miners\\MinerIcon_1.dds",                    256,  256),
            new SpriteTexture("M2",    "Textures\\FactionLogo\\Miners\\MinerIcon_2.dds",                    256,  256),
            new SpriteTexture("M3",    "Textures\\FactionLogo\\Miners\\MinerIcon_3.dds",                    256,  256),
            new SpriteTexture("M4",    "Textures\\FactionLogo\\Miners\\MinerIcon_4.dds",                    256,  256),
            new SpriteTexture("O1",    "Textures\\FactionLogo\\Others\\OtherIcon_1.dds",                    256,  256),
            new SpriteTexture("O10",   "Textures\\FactionLogo\\Others\\OtherIcon_10.dds",                   256,  256),
            new SpriteTexture("O11",   "Textures\\FactionLogo\\Others\\OtherIcon_11.dds",                   256,  256),
            new SpriteTexture("O12",   "Textures\\FactionLogo\\Others\\OtherIcon_12.dds",                   256,  256),
            new SpriteTexture("O13",   "Textures\\FactionLogo\\Others\\OtherIcon_13.dds",                   256,  256),
            new SpriteTexture("O14",   "Textures\\FactionLogo\\Others\\OtherIcon_14.dds",                   256,  256),
            new SpriteTexture("O15",   "Textures\\FactionLogo\\Others\\OtherIcon_15.dds",                   256,  256),
            new SpriteTexture("O16",   "Textures\\FactionLogo\\Others\\OtherIcon_16.dds",                   256,  256),
            new SpriteTexture("O17",   "Textures\\FactionLogo\\Others\\OtherIcon_17.dds",                   256,  256),
            new SpriteTexture("O18",   "Textures\\FactionLogo\\Others\\OtherIcon_18.dds",                   256,  256),
            new SpriteTexture("O19",   "Textures\\FactionLogo\\Others\\OtherIcon_19.dds",                   256,  256),
            new SpriteTexture("O2",    "Textures\\FactionLogo\\Others\\OtherIcon_2.dds",                    256,  256),
            new SpriteTexture("O20",   "Textures\\FactionLogo\\Others\\OtherIcon_20.dds",                   256,  256),
            new SpriteTexture("O21",   "Textures\\FactionLogo\\Others\\OtherIcon_21.dds",                   256,  256),
            new SpriteTexture("O22",   "Textures\\FactionLogo\\Others\\OtherIcon_22.dds",                   256,  256),
            new SpriteTexture("O23",   "Textures\\FactionLogo\\Others\\OtherIcon_23.dds",                   256,  256),
            new SpriteTexture("O24",   "Textures\\FactionLogo\\Others\\OtherIcon_24.dds",                   256,  256),
            new SpriteTexture("O26",   "Textures\\FactionLogo\\Others\\OtherIcon_26.dds",                   256,  256),
            new SpriteTexture("O27",   "Textures\\FactionLogo\\Others\\OtherIcon_27.dds",                   256,  256),
            new SpriteTexture("O28",   "Textures\\FactionLogo\\Others\\OtherIcon_28.dds",                   256,  256),
            new SpriteTexture("O29",   "Textures\\FactionLogo\\Others\\OtherIcon_29.dds",                   256,  256),
            new SpriteTexture("O3",    "Textures\\FactionLogo\\Others\\OtherIcon_3.dds",                    256,  256),
            new SpriteTexture("O30",   "Textures\\FactionLogo\\Others\\OtherIcon_30.dds",                   256,  256),
            new SpriteTexture("O31",   "Textures\\FactionLogo\\Others\\OtherIcon_31.dds",                   256,  256),
            new SpriteTexture("O32",   "Textures\\FactionLogo\\Others\\OtherIcon_32.dds",                   256,  256),
            new SpriteTexture("O33",   "Textures\\FactionLogo\\Others\\OtherIcon_33.dds",                   256,  256),
            new SpriteTexture("O4",    "Textures\\FactionLogo\\Others\\OtherIcon_4.dds",                    256,  256),
            new SpriteTexture("O5",    "Textures\\FactionLogo\\Others\\OtherIcon_5.dds",                    256,  256),
            new SpriteTexture("O6",    "Textures\\FactionLogo\\Others\\OtherIcon_6.dds",                    256,  256),
            new SpriteTexture("O7",    "Textures\\FactionLogo\\Others\\OtherIcon_7.dds",                    256,  256),
            new SpriteTexture("O8",    "Textures\\FactionLogo\\Others\\OtherIcon_8.dds",                    256,  256),
            new SpriteTexture("O9",    "Textures\\FactionLogo\\Others\\OtherIcon_9.dds",                    256,  256),
            new SpriteTexture("ICP",   "Textures\\FactionLogo\\PirateIcon.dds",                             256,  256),
            new SpriteTexture("SPD",   "Textures\\FactionLogo\\Spiders.dds",                                256,  256),
            new SpriteTexture("ICT1",  "Textures\\FactionLogo\\Traders\\TraderIcon_1.dds",                  256,  256),
            new SpriteTexture("ICT2",  "Textures\\FactionLogo\\Traders\\TraderIcon_2.dds",                  256,  256),
            new SpriteTexture("ICT3",  "Textures\\FactionLogo\\Traders\\TraderIcon_3.dds",                  256,  256),
            new SpriteTexture("ICT4",  "Textures\\FactionLogo\\Traders\\TraderIcon_4.dds",                  256,  256),
            new SpriteTexture("ICT5",  "Textures\\FactionLogo\\Traders\\TraderIcon_5.dds",                  256,  256),
            new SpriteTexture("TRI",   "Triangle",                                                          512,  512),
            new SpriteTexture("UVC",   "UVChecker",                                                        1024, 1024),
            new SpriteTexture("WS",    "White screen",                                                        4,    4)
        };
    }
}
