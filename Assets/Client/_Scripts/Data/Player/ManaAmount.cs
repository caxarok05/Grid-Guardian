using System;

namespace Scripts.Data.Player
{
    [Serializable]
    public class ManaAmount
    {
        public int currentMana;

        public ManaAmount() { }
        public ManaAmount(int currentMana) => this.currentMana = currentMana;
    }
}