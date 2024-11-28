using Scripts.Data;

namespace Scripts.Services.PersistantProgress
{
    public interface IPersistantProgressService
    {
        PlayerProgress progress { get; set; }
    }
}