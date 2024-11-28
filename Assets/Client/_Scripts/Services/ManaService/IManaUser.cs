using System;

namespace Scripts.Services.ManaService
{
    public interface IManaUser
    {
        int Mana { get; set; }
        void OnManaChanged(int amount);
        Action Changed { get; set; }

    }
}