using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Services.ManaService
{
    public class ManaManager : IManaManager
    {
        private Dictionary<IManaUser, int> manaUsers = new Dictionary<IManaUser, int>();

        public void RegisterUser(IManaUser user, int initialMana)
        {
            if (!manaUsers.ContainsKey(user))
            {
                manaUsers[user] = initialMana;
                user.OnManaChanged(initialMana);
            }
        }

        public void ConsumeMana(IManaUser user, int amount)
        {
            if (manaUsers.TryGetValue(user, out int currentMana) && currentMana >= amount)
            {
                manaUsers[user] -= amount;
                user.OnManaChanged(manaUsers[user]);
            }
            else
            {
                Debug.Log("Недостаточно маны");
            }
        }

        public void RestoreMana(IManaUser user, int amount)
        {
            if (manaUsers.TryGetValue(user, out int currentMana))
            {
                manaUsers[user] += amount;
                user.OnManaChanged(manaUsers[user]);
            }
        }

        public int GetMana(IManaUser user)
        {
            return manaUsers.TryGetValue(user, out int currentMana) ? currentMana : 0;
        }
    }
}