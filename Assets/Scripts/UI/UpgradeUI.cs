﻿using System;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private UpgradeSystem upgradeSystem;
        [SerializeField] private List<UpgradeSlotUI> upgradeSlots;
        [SerializeField] private TextMeshProUGUI levelTextField;

        private void OnEnable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress += HandleSelection;
            }
        }

        private void OnDisable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress -= HandleSelection;
            }
        }

        private void HandleSelection(UpgradeSlotUI usui)
        {
            upgradeSystem.ApplyUpgrade(usui.Upgrade, upgradeSystem.ActivePlayer);
            Close();
        }

        private void Close()
        {
            gameObject.SetActive(false);
            enabled = false;
        }

        public void Refresh()
        {
            foreach (var s in upgradeSlots)
            {
                s.UpdateGraphics();
            }

            levelTextField.text = upgradeSystem.ActivePlayer.Level.ToString();
        }

        public void SetUpgrades(List<IUpgrade> upgrades)
        {
            for (int i = 0; i < upgradeSlots.Count; i++)
            {
                upgradeSlots[i].Upgrade = null;
            }
            for (int i = 0; i < Math.Min(upgrades.Count, upgradeSlots.Count); i++)
            {
                upgradeSlots[i].Upgrade = upgrades[i];
            }
        }
    }
}