using Scripts.Data;

namespace Scripts.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress progress { get; set; }
    }
}