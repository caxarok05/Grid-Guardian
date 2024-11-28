using Scripts.Infrastructure.Factory;
using System.Threading.Tasks;

namespace Scripts.UI.UIFactory
{
    public interface IUIFactory
    {
        void CreatePause();
        Task CreateUIRoot();
        void SetFactory(IGameFactory gameFactory);
    }
}
