using Scripts.Infrastructure.Factory;
using Scripts.Logic.Hero;
using Scripts.Services.GridService;
using Scripts.Services.ManaService;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Enemy
{
    public class Attack : MonoBehaviour
    {
        public int _manaToKill;
        public int _rewardMana;

        public GameObject manaDrop;

        public Action Happened;

        private bool _killed;

        private IManaManager _manaManager;
        private IGridManager _gridManager;
        private Transform _heroTransform;

        [Inject]
        public void Construct(IManaManager manaManager, IGridManager gridManager, IGameFactory gameFactory)
        {
            _manaManager = manaManager;
            _gridManager = gridManager;
            _heroTransform = gameFactory.HeroGameObject.transform;

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (CheckAvailable(collision))
            {
                _killed = true;
                Fight(collision.GetComponent<IManaUser>());
            }
        }

        private void Fight(IManaUser player)
        {
            if (_manaManager.GetMana(player) >= _manaToKill)
            {
                Kill(player);
            }
        }

        private bool CheckAvailable(Collider2D collision) =>          
            collision.transform == _heroTransform
            && _gridManager.GetTileAtPosition(transform.position) == collision.GetComponent<PlayerMovement>()?.GetPlayerPosition()
            && !_killed;

        private void Kill(IManaUser player)
        {
            _manaManager.ConsumeMana(player, _manaToKill);
            _manaManager.RestoreMana(player, _rewardMana);
            Happened?.Invoke();
            PlayDeathAnimation();
            StartCoroutine(DestroyTimer());
        }

        private void PlayDeathAnimation()
        {
            GameObject drop = Instantiate(manaDrop, transform.position, Quaternion.identity, gameObject.transform);
            drop.GetComponentInChildren<TextMeshProUGUI>().text = _rewardMana.ToString();
            drop.GetComponent<Animation>().Play();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }

    }
}