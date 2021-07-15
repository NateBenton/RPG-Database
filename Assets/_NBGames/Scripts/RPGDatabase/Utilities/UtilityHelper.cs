using System;
using System.Collections.Generic;
using _NBGames.Scripts.RPGDatabase.Misc;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Utilities
{
    public static class UtilityHelper
    {
        public static bool CreatingClass = false, CreatingHero = false, ShowHeroStats = true;
        public static bool CreatingSkill = false, CreatingCurve = false;
        public static bool CreatingWeapon = false, CreatingArmor = false;
        
        public static readonly List<Hero> HeroAssetList = new List<Hero>();
        public static readonly List<HeroClass> ClassAssetList = new List<HeroClass>();
        public static readonly List<Weapon> WeaponAssetList = new List<Weapon>();
        public static readonly List<Armor> ArmorAssetList = new List<Armor>();
        public static readonly List<Skill> SkillAssetList = new List<Skill>();
        public static readonly List<LevelCurve> CurveAssetList = new List<LevelCurve>();

        public static StartingParty StartingParty;
        
        public static List<string> HeroNameList = new List<string>();
        public static List<string> HeroNameListMod = new List<string>();
        public static List<string> ClassNameList = new List<string>();
        public static List<string> ClassNameListRaw = new List<string>();
        public static List<string> WeaponNameList = new List<string>();
        public static List<string> WeaponNameListRaw = new List<string>();
        public static List<string> ArmorNameList = new List<string>();
        public static List<string> ArmorNameListRaw = new List<string>();
        public static List<string> SkillNameList = new List<string>();
        public static List<string> SkillNameListRaw = new List<string>();
        public static List<string> CurveNameList = new List<string>();
        public static List<string> CurveNameListRaw = new List<string>();
        
        public static readonly List<string> HeroGuidList = new List<string>();
        public static readonly List<string> SkillGuidList = new List<string>();
        public static readonly List<string> ClassGuidList = new List<string>();
        public static readonly List<string> CurveGuidList = new List<string>();
        public static readonly List<string> WeaponGuidList = new List<string>();
        public static readonly List<string> ArmorGuidList = new List<string>();

        public static int CurrentHeroTab, CurrentSkillTab, CurrentClassTab, CurrentCurveTab;
        public static int CurrentWeaponTab, CurrentArmorTab;
        public static readonly string[] DatabaseTabNames = {"Heroes", "Classes", 
           "Skills",  "Level Curves", "Weapons", "Armors", "Party"};
        
        private static string[] _assetList;
        public static Vector2 ScrollHeroTab, ScrollHeroContainer, ScrollSkillTab, ScrollSkillContainer, ScrollClassTab;
        public static Vector2 ScrollClassContainer, ScrollCurveTab, ScrollCurveContainer;
        public static Vector2 ScrollWeaponContainer, ScrollArmorContainer, ScrollWeaponTab, ScrollArmorTab;
        public static int CurrentDatabaseTab;
        public static int PreviousDatabaseTab;

        private const string Glyph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string NewHeroName = "New Hero";
        public static string NewSkillName = "New Skill";
        public static string NewClassName = "New Class";
        public static string NewCurveName = "New Level Curve";
        public static string NewWeaponName = "New Weapon";
        public static string NewArmorName = "New Armor";

        public static Action onRefreshWindow;
        public static void RefreshWindow()
        {
            onRefreshWindow?.Invoke();
        }
        
       public static void GetAssetData(int dataType)
        {
            _assetList = dataType switch
            {
                0 => AssetDatabase.FindAssets("t:Hero"),
                1 => AssetDatabase.FindAssets("t:HeroClass"),
                2 => AssetDatabase.FindAssets("t:Weapon"),
                3 => AssetDatabase.FindAssets("t:Armor"),
                4 => AssetDatabase.FindAssets("t:Skill"),
                5 => AssetDatabase.FindAssets("t:LevelCurve"),
                6 => AssetDatabase.FindAssets("t:StartingParty"),
                _ => _assetList
            };

            for (var i = 0; i < _assetList.Length; i++)
            {
                _assetList[i] = AssetDatabase.GUIDToAssetPath(_assetList[i]);
                
                switch (dataType)
                {
                    case 0:
                        HeroGuidList.Add(_assetList[i]);
                        HeroAssetList.
                            Add((Hero)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(Hero)));
                        break;
                    case 1:
                        ClassGuidList.Add(_assetList[i]);
                        ClassAssetList.
                            Add((HeroClass)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(HeroClass)));
                        break;
                    case 2:
                        WeaponGuidList.Add(_assetList[i]);
                        WeaponAssetList.
                            Add((Weapon)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(Weapon)));
                        break;
                    case 3:
                        ArmorGuidList.Add(_assetList[i]);
                        ArmorAssetList.
                            Add((Armor)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(Armor)));
                        break;
                    case 4:
                        SkillGuidList.Add(_assetList[i]);
                        SkillAssetList.
                            Add((Skill)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(Skill)));
                        break;
                    case 5:
                        CurveGuidList.Add(_assetList[i]);
                        CurveAssetList.
                            Add((LevelCurve)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(LevelCurve)));
                        break;
                    case 6:
                        StartingParty = (StartingParty)AssetDatabase.
                            LoadAssetAtPath(_assetList[i], typeof(StartingParty));
                        break;
                }
            }

            switch (dataType)
            {
                case 0:
                    HeroNameListMod.Add("None");
                    foreach (var hero in HeroAssetList)
                    {
                        HeroNameList.Add(hero.Name);
                        HeroNameListMod.Add(hero.Name);
                    }

                    break;
                case 1:
                    ClassNameList.Add("None");
                    foreach (var heroClass in ClassAssetList)
                    {
                        ClassNameList.Add(heroClass.Name);
                        ClassNameListRaw.Add(heroClass.Name);
                    }

                    break;
                case 2:
                    WeaponNameList.Add("None");
                    foreach (var weapon in WeaponAssetList)
                    {
                        WeaponNameList.Add(weapon.Name);
                        WeaponNameListRaw.Add(weapon.Name);
                    }

                    break;
                case 3:
                    ArmorNameList.Add("None");
                    foreach (var armor in ArmorAssetList)
                    {
                        ArmorNameList.Add(armor.Name);
                        ArmorNameListRaw.Add(armor.Name);
                    }

                    break;
                case 4:
                    SkillNameList.Add("None");
                    foreach (var skill in SkillAssetList)
                    {
                        SkillNameList.Add(skill.Name);
                        SkillNameListRaw.Add(skill.Name);
                    }

                    break;
                case 5:
                    CurveNameList.Add("None");
                    foreach (var curve in CurveAssetList)
                    {
                        CurveNameList.Add(curve.CurveName);
                        CurveNameListRaw.Add(curve.CurveName);
                    }

                    break;
            }
        }
       
       public static string GenerateFileName(int dataType)
       {
           var randomString = "";
           for (var i = 0; i < 30; i++)
           {
               randomString += Glyph[UnityEngine.Random.Range(0, Glyph.Length)];
           }

           return dataType switch
           {
               1 => $"Class_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               2 => $"Weapon_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               3 => $"Armor_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               4 => $"Skill_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               5 => $"LevelCurve_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               _ => $"Hero_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}"
           };
       }

       public static void RemoveDeletedHeroesFromParty()
       {
           if (StartingParty.PartyIndexes[0] > HeroAssetList.Count)
           {
               StartingParty.PartyIndexes[0] = 0;
               StartingParty.Party[0] = null;
           }
            
           if (StartingParty.PartyIndexes[1] > HeroAssetList.Count)
           {
               StartingParty.PartyIndexes[1] = 0;
               StartingParty.Party[1] = null;
           }

           if (StartingParty.PartyIndexes[2] <= HeroAssetList.Count) return;
           StartingParty.PartyIndexes[2] = 0;
           StartingParty.Party[2] = null;
       }
       
       public static void ClearAssetData()
       {
           HeroAssetList.Clear();
           HeroNameList.Clear();
           HeroGuidList.Clear();
           HeroNameListMod.Clear();
           ClassAssetList.Clear();
           WeaponAssetList.Clear();
           SkillAssetList.Clear();
           
           ClassNameList.Clear();
           WeaponNameList.Clear();
           SkillNameList.Clear();
           
           SkillGuidList.Clear();
           ClassGuidList.Clear();
           ClassNameListRaw.Clear();
           SkillNameListRaw.Clear();
           CurveNameList.Clear();
           CurveNameListRaw.Clear();
           CurveGuidList.Clear();
           CurveAssetList.Clear();
           WeaponNameListRaw.Clear();
           ArmorNameListRaw.Clear();
           ArmorNameList.Clear();
           ArmorGuidList.Clear();
           ArmorAssetList.Clear();
           WeaponGuidList.Clear();
           StartingParty = null;
       }
    }
}
