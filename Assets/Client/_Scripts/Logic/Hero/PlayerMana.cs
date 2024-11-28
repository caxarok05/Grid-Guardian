using Scripts.Data;
using Scripts.Services.ManaService;
using Scripts.Services.SaveLoad;
using System;
using UnityEngine;

namespace Scripts.Logic.Hero
{
    public class PlayerMana : MonoBehaviour, IManaUser, ISavedProgress
    {
        public int Mana { get; set; }
        public Action Changed { get; set; }

        public void OnManaChanged(int amount)
        {
            Mana = amount;
            Changed?.Invoke();
        }
        public int GetCurrentMana()
        {
            Changed?.Invoke();
            return Mana;
        }

        public void UpdateProgress(PlayerProgress progress) => progress.manaAmount.currentMana = Mana;

        public void LoadProgress(PlayerProgress progress) => Mana = progress.manaAmount.currentMana;


    }
}