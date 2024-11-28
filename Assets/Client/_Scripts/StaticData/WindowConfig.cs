using Scripts.Services.WindowService;
using Scripts.UI.Windows;
using System;

namespace Scripts.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId windowId; 
        public WindowBase prefab; 
    }
}