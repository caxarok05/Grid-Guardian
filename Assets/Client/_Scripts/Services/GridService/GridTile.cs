using UnityEngine;

namespace Scripts.Services.GridService
{
    public class GridTile : MonoBehaviour
    {
        public bool _isFree = true;

        [SerializeField] private Color _baseColor, _offsetColor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;


        public void Init(bool isOffset) => _renderer.color = isOffset ? _offsetColor : _baseColor;

        private void OnMouseEnter() => _highlight.SetActive(true);

        private void OnMouseExit() => _highlight.SetActive(false);
    }
}