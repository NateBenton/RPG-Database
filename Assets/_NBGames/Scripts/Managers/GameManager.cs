using System;
using System.Collections.Generic;
using _NBGames.Scripts.RPGDatabase.Misc;
using _NBGames.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _NBGames.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Party Configuration")]
        [SerializeField] private StartingParty _startingParty;

        [Header("Hero Text Fields")] 
        [SerializeField] private Image _heroImage;
        [SerializeField] private TextMeshProUGUI _heroNameText;
        [SerializeField] private TextMeshProUGUI _heroDescriptionText;
        [SerializeField] private TextMeshProUGUI _heroClassText;

        [Header("Level Fields")] 
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Slider _expSlider;

        [Header("HP/MP Fields")] 
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _mpText;
        
        [Header("Stat Fields")]
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _defenseText;
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private TextMeshProUGUI _armorNameText;

        [Header("UI Controls")] 
        [SerializeField] private TMP_InputField _expInputField;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _weaponButton;
        [SerializeField] private Button _armorButton;

        private List<Hero> _party = new List<Hero>();
        private Hero _hero;
        private int _heroIndex;
        private int _heroAttackValue, _heroDefenseValue;
        private TextMeshProUGUI _buttonText;

        private void Awake()
        {
            if (!_startingParty)
            {
                Debug.LogWarning($"Starting Party is null on {gameObject.name}");
            }
        }

        private void Start()
        {
            PopulateHeroes();
            PopulateHeroInfo();
        }

        private void PopulateHeroes()
        {
            foreach (var hero in _startingParty.Party)
            {
                if (!hero) continue;
                var newHero = ScriptableObject.CreateInstance<Hero>();

                newHero.Name = hero.Name;
                newHero.Description = hero.Description;
                newHero.Sprite = hero.Sprite;
                
                newHero.Stats = new Stats
                {
                    Attack = hero.Class.LevelCurve.Attack[GetLevelAdjusted(hero)],
                    Defense = hero.Class.LevelCurve.Defense[GetLevelAdjusted(hero)], 
                    Experience = hero.Stats.Experience,
                    CurrentLevel = hero.Stats.CurrentLevel, 
                    CurrentHp = hero.Class.LevelCurve.Hp[GetLevelAdjusted(hero)], 
                    CurrentMp = hero.Class.LevelCurve.Mp[GetLevelAdjusted(hero)]
                };
                
                newHero.Stats.MaxHp = newHero.Stats.CurrentHp;
                newHero.Stats.MaxMp = newHero.Stats.CurrentMp;
                

                newHero.Class = hero.Class;
                newHero.Weapon = hero.Weapon;
                newHero.Armor = hero.Armor;
                _party.Add(newHero);
            }
            
            if (_party.Count <= 1)
            {
                _nextButton.interactable = false;
            }
        }

        private void PopulateHeroInfo()
        {
            _hero = _party[_heroIndex];

            _heroDescriptionText.text = _hero.Description;
            _heroImage.sprite = _hero.Sprite;
            _heroNameText.text = _hero.Name;
            _heroClassText.text = _hero.Class.Name;
            _currentLevelText.text = _hero.Stats.CurrentLevel.ToString();
            _expText.text = GetExpRemaining();
            _hpText.text = $"HP: {_hero.Stats.CurrentHp} / {_hero.Stats.MaxHp}";
            _mpText.text = $"MP: {_hero.Stats.CurrentMp} / {_hero.Stats.MaxMp}";
            AttackDefendInfo();

            if (_hero.Weapon)
            {
                _weaponNameText.text = $"{_hero.Weapon.Name} - {_hero.Weapon.AttackModifier}";
                
                _buttonText = _weaponButton.GetComponentInChildren<TextMeshProUGUI>();
                if (_buttonText == null) return;

                _buttonText.text = "Remove Weapon";
            }
            else
            {
                _weaponNameText.text = null;
                _buttonText = _weaponButton.GetComponentInChildren<TextMeshProUGUI>();

                if (_buttonText == null) return;
                _buttonText.text = "Equip\n Weapon";
            }

            if (_hero.Armor != null)
            {
                _armorNameText.text = $"{_hero.Armor.Name} - {_hero.Armor.DefenseModifier}";
                
                _buttonText = _armorButton.GetComponentInChildren<TextMeshProUGUI>();
                if (_buttonText == null) return;

                _buttonText.text = "Remove Armor";
            }
            else
            {
                _armorNameText.text = null;
                _buttonText = _armorButton.GetComponentInChildren<TextMeshProUGUI>();

                if (_buttonText == null) return;
                _buttonText.text = "Equip Armor";
            }

            _weaponButton.interactable = _startingParty.Party[_heroIndex].Weapon != null;
            _armorButton.interactable = _startingParty.Party[_heroIndex].Armor != null;

            _expSlider.maxValue = _hero.Class.LevelCurve.ExpToNextLevel[GetLevelAdjusted(_hero)];
            _expSlider.value = _hero.Stats.Experience;
        }

        private void AttackDefendInfo()
        {
            _heroAttackValue = _hero.Stats.Attack;
            _heroDefenseValue = _hero.Stats.Defense;

            if (_hero.Weapon != null)
            {
                _heroAttackValue += _hero.Weapon.AttackModifier;
                _heroDefenseValue += _hero.Weapon.DefenseModifier;
            }

            if (_hero.Armor != null)
            {
                _heroAttackValue += _hero.Armor.AttackModifier;
                _heroDefenseValue += _hero.Armor.DefenseModifier;
            }

            _attackText.text = _heroAttackValue.ToString();

            _defenseText.text = _heroDefenseValue.ToString();
        }

        private string GetExpRemaining()
        {
            var currentLevel = GetLevelAdjusted(_hero);

            var remaining = _hero.Class.LevelCurve
                .ExpToNextLevel[currentLevel];

            var current = _hero.Stats.Experience;

            return $"{current} / {remaining}";
        }

        private int GetLevelAdjusted(Hero hero)
        {
            return hero.Stats.CurrentLevel - 1;
        }
        
        public void NextButton()
        {
            ++_heroIndex;
            _previousButton.interactable = true;
            
            if (_heroIndex == _party.Count -1)
            {
                _nextButton.interactable = false;
            }
            
            PopulateHeroInfo();
        }

        public void PreviousButton()
        {
            --_heroIndex;
            _nextButton.interactable = true;

            if (_heroIndex == 0)
            {
                _previousButton.interactable = false;
            }
            
            PopulateHeroInfo();
        }

        public void WeaponButton()
        {
            if (_hero.Weapon != null)
            {
                _hero.Weapon = null;
                var text = _weaponButton.GetComponentInChildren<TextMeshProUGUI>();

                if (text == null) return;
                text.text = "Equip\n Weapon";
                
                PopulateHeroInfo();
            }
            else
            {
                if (_startingParty.Party[_heroIndex].Weapon == null) return;
                _hero.Weapon = _startingParty.Party[_heroIndex].Weapon;
                var text = _weaponButton.GetComponentInChildren<TextMeshProUGUI>();
                
                if (text == null) return;
                text.text = "Remove Weapon";
                
                PopulateHeroInfo();
            }
        }

        public void ArmorButton()
        {
            if (_hero.Armor != null)
            {
                _hero.Armor = null;
                var text = _armorButton.GetComponentInChildren<TextMeshProUGUI>();

                if (text == null) return;
                text.text = "Equip\n Armor";
                
                PopulateHeroInfo();
            }
            else
            {
                if (_startingParty.Party[_heroIndex].Armor == null) return;
                _hero.Armor = _startingParty.Party[_heroIndex].Armor;
                var text = _armorButton.GetComponentInChildren<TextMeshProUGUI>();
                
                if (text == null) return;
                text.text = "Remove Armor";
                
                PopulateHeroInfo();
            }
        }

        public void AddExpButton()
        {
            if (_expInputField.text == "") return;
            AddExperience(int.Parse(_expInputField.text));
            _expInputField.text = null;
        }

        private void AddExperience(int expToAdd)
        {
            var expToNext = _hero.Class.LevelCurve.ExpToNextLevel[GetLevelAdjusted(_hero)] - (_hero.Stats.Experience + expToAdd);

            if (expToNext <= 0)
            {
                LevelUpHero(Mathf.Abs(expToNext));
            }
            else
            {
                _hero.Stats.Experience += Mathf.Abs(expToAdd);
                PopulateHeroInfo();
            }
        }

        private void LevelUpHero(int exp)
        {
            _hero.Stats.CurrentLevel++;
            _hero.Stats.Experience = 0;

            var expToNext = _hero.Class.LevelCurve.ExpToNextLevel[GetLevelAdjusted(_hero)] - exp;

            if (expToNext <= 0)
            {
                LevelUpHero(Mathf.Abs(expToNext));
            }
            else
            {
                _hero.Stats.Experience += Mathf.Abs(exp);
                PopulateHeroInfo();
            }
        }
    }
}
