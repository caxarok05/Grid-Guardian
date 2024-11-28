using Scripts.Services.WindowService;
using Scripts.StaticData;

namespace Scripts.Services.StaticDataService
{
    public interface IStaticDataService
    {
        MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
        LocationStaticData ForLocation(string level);
        WindowConfig ForWindow(WindowId windowId);
    }
}