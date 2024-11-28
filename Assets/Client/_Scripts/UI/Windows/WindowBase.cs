using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button closeButton;

        public void Construct() { }

        private void Awake() => OnAwake();

        private void Start()
        {
            Init();
            SubscribeUpdates();
        }

        private void OnDestroy() =>
          Cleanup();

        protected virtual void OnAwake()
        {
            closeButton.onClick.AddListener(() => Destroy(gameObject));
        }

        protected virtual void Init() { }
        protected virtual void SubscribeUpdates() { }
        protected virtual void Cleanup() { }
    }
}
