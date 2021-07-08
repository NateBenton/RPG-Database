using System;
using System.Collections.Generic;
using _NBGames.Scripts.ScriptableObjects;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Utilities
{
    public static class UtilityHelper
    {
        public static bool ShowHeroGeneralSettings;
        public static readonly List<Hero> HeroAssetList = new List<Hero>();
        public static readonly List<HeroClass> ClassAssetList = new List<HeroClass>();
        public static List<string> HeroNameList = new List<string>();
        public static List<string> ClassNameList = new List<string>();
        
        public static int CurrentHeroTab;
        public static readonly string[] DatabaseTabNames = {"Heroes", "Classes", "Equipment"};
        public static string[] AssetList;
        public static Vector2 ScrollHeroTab;
        public static int CurrentDatabaseTab;
    }
}
