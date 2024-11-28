using Scripts.Data;
using Scripts.Infrastructure.Factory;
using Scripts.Services.ManaService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI counter;

        private IManaUser _user;

        [Inject]
        public void Construct(IGameFactory factory)
        {
            _user = factory.HeroGameObject.GetComponent<IManaUser>();
            _user.Changed += UpdateCounter;
        }

        private void Start() => UpdateCounter();
        private void UpdateCounter() => counter.text = $"{_user.Mana}";
    }
}