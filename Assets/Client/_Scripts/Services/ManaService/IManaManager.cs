

namespace Scripts.Services.ManaService
{
    public interface IManaManager
    {
        void RegisterUser(IManaUser user, int initialMana);
        void ConsumeMana(IManaUser user, int amount);
        void RestoreMana(IManaUser user, int amount);
        int GetMana(IManaUser user);

    }
}