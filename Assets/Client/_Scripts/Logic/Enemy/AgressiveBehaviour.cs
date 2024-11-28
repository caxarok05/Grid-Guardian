using UnityEngine;

namespace Scripts.Logic.Enemy
{
    public class AgressiveBehaviour : MonoBehaviour
    {
        public Sprite calmSprite;
        public Sprite angrySprite;

        public void MakeAngry() =>
            gameObject.GetComponent<SpriteRenderer>().sprite = angrySprite;

        public void MakeCalm() =>
            gameObject.GetComponent<SpriteRenderer>().sprite = calmSprite;

    }
}