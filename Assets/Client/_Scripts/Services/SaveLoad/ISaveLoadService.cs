using Scripts.Data;

namespace Scripts.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}