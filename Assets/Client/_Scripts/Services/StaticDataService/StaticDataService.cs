using Scripts.Services.WindowService;
using Scripts.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataMonstersPath = "StaticData/Monsters";
        private const string StaticDataLocationPath = "StaticData/Location";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";

        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters = new Dictionary<MonsterTypeId, MonsterStaticData>();
        private Dictionary<string, LocationStaticData> _location = new Dictionary<string, LocationStaticData>();
        private Dictionary<WindowId, WindowConfig> _windowConfigs = new Dictionary<WindowId, WindowConfig>();


        public StaticDataService()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(StaticDataMonstersPath)
                .ToDictionary(x => x.monsterTypeID, x => x);

            _location = Resources.LoadAll<LocationStaticData>(StaticDataLocationPath)
                .ToDictionary(x => x.level, x => x);

            _windowConfigs = Resources.Load<WindowStaticData>(StaticDataWindowsPath)
                .config.ToDictionary(x => x.windowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) =>
            _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData)
            ? staticData
            : null;

        public LocationStaticData ForLocation(string level) =>
            _location.TryGetValue(level, out LocationStaticData staticData)
            ? staticData
            : null;

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig staticData)
            ? staticData
            : null;

    }
}