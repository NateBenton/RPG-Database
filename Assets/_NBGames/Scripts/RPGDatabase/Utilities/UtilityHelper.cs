using System;
using System.Collections.Generic;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Utilities
{
    public static class UtilityHelper
    {
        public static bool ShowHeroGeneralSettings = true, CreatingHero = false, ShowHeroStats = true;
        public static bool ShowHeroLevelCurve = true;
        public static readonly List<Hero> HeroAssetList = new List<Hero>();
        public static readonly List<HeroClass> ClassAssetList = new List<HeroClass>();
        public static readonly List<Weapon> WeaponAssetList = new List<Weapon>();
        public static readonly List<Armor> ArmorAssetList = new List<Armor>();
        
        public static readonly List<string> HeroNameList = new List<string>();
        public static readonly List<string> ClassNameList = new List<string>();
        public static readonly List<string> WeaponNameList = new List<string>();
        public static readonly List<string> ArmorNameList = new List<string>();
        public static readonly List<string> HeroGuidList = new List<string>();

        public static int CurrentHeroTab;
        public static readonly string[] DatabaseTabNames = {"Heroes", "Classes", "Weapons", "Armors"};
        private static string[] _assetList;
        public static Vector2 ScrollHeroTab, ScrollHeroContainer;
        public static int CurrentDatabaseTab;

        private const string Glyph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string NewHeroName = "New Hero";

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
               _ => $"Hero_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}"
           };
       }
       
       public static void ClearAssetData()
       {
           HeroAssetList.Clear();
           ClassAssetList.Clear();
           HeroNameList.Clear();
           ClassNameList.Clear();
           HeroGuidList.Clear();
       }
    }
}
