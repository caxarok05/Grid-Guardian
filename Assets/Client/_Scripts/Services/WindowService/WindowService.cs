using Scripts.UI.UIFactory;

namespace Scripts.Services.WindowService
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Unknown:

                    break;
                case WindowId.Pause:
                    _uiFactory.CreatePause();
                    break;
            }
        }
    }
}
