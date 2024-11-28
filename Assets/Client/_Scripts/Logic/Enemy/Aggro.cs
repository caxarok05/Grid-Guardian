using System.Collections;
using UnityEngine;

namespace Scripts.Logic.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver triggerObserver;
        public AgressiveBehaviour agressiveBehaviour;

        [SerializeField] private float Cooldown;

        private bool _hasAggroTarget;

        private WaitForSeconds _switchAggroOffAfterCooldown;
        private Coroutine _aggroCoroutine;

        private void Start()
        {
            _switchAggroOffAfterCooldown = new WaitForSeconds(Cooldown);

            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            agressiveBehaviour.MakeCalm();
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
            triggerObserver.TriggerExit -= TriggerExit;
            StopAggroCoroutine();
        }

        private void TriggerEnter(Collider2D obj)
        {
            if (_hasAggroTarget || !gameObject.activeInHierarchy) return;

            StopAggroCoroutine();

            SwitchAggroOn();
        }

        private void TriggerExit(Collider2D obj)
        {
            if (!_hasAggroTarget || !gameObject.activeInHierarchy) return;

            _aggroCoroutine = StartCoroutine(SwitchAggroOffAfterCooldown());
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            } 
        }

        private IEnumerator SwitchAggroOffAfterCooldown()
        {
            yield return _switchAggroOffAfterCooldown;

            if (gameObject.activeInHierarchy)
                SwitchAggroOff();
        }

        private void SwitchAggroOn()
        {
            _hasAggroTarget = true;
            agressiveBehaviour?.MakeAngry();
        }

        private void SwitchAggroOff()
        {
            _hasAggroTarget = false;
            agressiveBehaviour?.MakeCalm();

        }
    }
}