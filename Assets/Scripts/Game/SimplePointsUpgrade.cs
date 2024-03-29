﻿using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SimplePointsUpgrade : IUpgrade
    {
        private Stat _type;
        private float _val;

        public SimplePointsUpgrade(int quantity)
        {
            _val = quantity;
        }
        
        public void Apply(PlayerManager player)
        {
            GameManager.Instance.PointUpgrades += 1;
        }

        public string GetUpgradeName()
        {
            return $"Gain {Math.Round(_val)} points.";
        }

        public string GetFlavourText()
        {
            return "";
        }

        public Sprite GetImage()
        {
            return null;
        }

        public string GetTypeID()
        {
            return "pts";
        }
    }
}