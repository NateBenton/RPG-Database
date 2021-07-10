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
        public static bool ShowHeroLevelCurve = true, CreatingSkill = false, CreatingCurve = false;
        public static readonly List<Hero> HeroAssetList = new List<Hero>();
        public static readonly List<HeroClass> ClassAssetList = new List<HeroClass>();
        public static readonly List<Weapon> WeaponAssetList = new List<Weapon>();
        public static readonly List<Armor> ArmorAssetList = new List<Armor>();
        public static readonly List<Skill> SkillAssetList = new List<Skill>();
        public static readonly List<LevelCurve> CurveAssetList = new List<LevelCurve>();
        
        public static List<string> HeroNameList = new List<string>();
        public static List<string> ClassNameList = new List<string>();
        public static List<string> ClassNameListRaw = new List<string>();
        public static readonly List<string> WeaponNameList = new List<string>();
        public static readonly List<string> ArmorNameList = new List<string>();
        public static List<string> SkillNameList = new List<string>();
        public static List<string> SkillNameListRaw = new List<string>();
        public static List<string> CurveNameList = new List<string>();
        public static List<string> CurveNameListRaw = new List<string>();
        
        public static readonly List<string> HeroGuidList = new List<string>();
        public static readonly List<string> SkillGuidList = new List<string>();
        public static readonly List<string> ClassGuidList = new List<string>();
        public static readonly List<string> CurveGuidList = new List<string>();

        public static int CurrentHeroTab, CurrentSkillTab, CurrentClassTab, CurrentCurveTab;
        public static readonly string[] DatabaseTabNames = {"Heroes", "Classes", 
           "Skills",  "Level Curves", "Weapons", "Armors"};
        
        private static string[] _assetList;
        public static Vector2 ScrollHeroTab, ScrollHeroContainer, ScrollSkillTab, ScrollSkillContainer, ScrollClassTab;
        public static Vector2 ScrollClassContainer, ScrollCurveTab, ScrollCurveContainer;
        public static int CurrentDatabaseTab;

        private const string Glyph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string NewHeroName = "New Hero";
        public static string NewSkillName = "New Skill";
        public static string NewClassName = "New Class";
        public static string NewCurveName = "New Level Curve";

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
                        WeaponAssetList.
                            Add((Weapon)AssetDatabase.
                                LoadAssetAtPath(_assetList[i], typeof(Weapon)));
                        break;
                    case 3:
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
                }
            }

            switch (dataType)
            {
                case 0:
                    foreach (var hero in HeroAssetList)
                    {
                        HeroNameList.Add(hero.HeroName);
                    }

                    break;
                case 1:
                    ClassNameList.Add("None");
                    foreach (var heroClass in ClassAssetList)
                    {
                        ClassNameList.Add(heroClass.ClassName);
                        ClassNameListRaw.Add(heroClass.ClassName);
                    }

                    break;
                case 2:
                    WeaponNameList.Add("None");
                    foreach (var weapon in WeaponAssetList)
                    {
                        WeaponNameList.Add(weapon.WeaponName);
                    }

                    break;
                case 3:
                    ArmorNameList.Add("None");
                    foreach (var armor in ArmorAssetList)
                    {
                        ArmorNameList.Add(armor.ArmorName);
                    }

                    break;
                case 4:
                    foreach (var skill in SkillAssetList)
                    {
                        SkillNameList.Add(skill.SkillName);
                        SkillNameListRaw.Add(skill.SkillName);
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
               2 => $"Skill_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               5 => $"LevelCurve_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
               _ => $"Hero_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}"
           };
       }
       
       public static void ClearAssetData()
       {
           HeroAssetList.Clear();
           ClassAssetList.Clear();
           WeaponAssetList.Clear();
           SkillAssetList.Clear();
           HeroNameList.Clear();
           ClassNameList.Clear();
           WeaponNameList.Clear();
           SkillNameList.Clear();
           HeroGuidList.Clear();
           SkillGuidList.Clear();
           ClassGuidList.Clear();
           ClassNameListRaw.Clear();
           SkillNameListRaw.Clear();
           CurveNameList.Clear();
           CurveNameListRaw.Clear();
           CurveGuidList.Clear();
           CurveAssetList.Clear();
       }
    }
}
