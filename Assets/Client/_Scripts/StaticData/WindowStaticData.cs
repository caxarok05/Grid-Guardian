using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> config;
    }
}