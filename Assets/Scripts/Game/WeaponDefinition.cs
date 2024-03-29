﻿using System;
using System.Collections.Generic;
using Projectiles;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LevelDefPair
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private int level;
        [SerializeField] private ProjectileMultiShooter shooter;
        [SerializeField] private string upgradeName;
        [SerializeField] private string upgradeDescription;
        [SerializeField] private Sprite image;

        public int Level => level;
        public ProjectileMultiShooter Shooter => shooter;
        public string UpgradeName => upgradeName;
        public string UpgradeDescription => upgradeDescription;
        public Sprite Image => image;
        public Projectile Projectile => projectile;
    }
    
    [Serializable]
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class WeaponDefinition : ScriptableObject
    {
        [SerializeField] private string lineName;
        [SerializeField] private List<LevelDefPair> weaponLevels;
        private Dictionary<int, LevelDefPair> _getWeaponOfLevel = new Dictionary<int, LevelDefPair>();

        public string LineName => lineName;
        public int MaxLevel => weaponLevels.Count;

        private void OnValidate()
        {
            RepopulateDictionary();
        }

        public ProjectileMultiShooter GetShooter(int level)
        {
            RepopulateDictionary();
            if (!_getWeaponOfLevel.ContainsKey(level)) return null;
            return _getWeaponOfLevel[level].Shooter;
        }

        private void RepopulateDictionary()
        {
            if (_getWeaponOfLevel == null) _getWeaponOfLevel = new Dictionary<int, LevelDefPair>();
            _getWeaponOfLevel.Clear();
            foreach (var w in weaponLevels)
            {
                _getWeaponOfLevel.Add(w.Level, w);
            }
        }

        public LevelDefPair GetWeaponInfoLevel(int i)
        {
            RepopulateDictionary();
            return _getWeaponOfLevel[i];
        }
    }
}