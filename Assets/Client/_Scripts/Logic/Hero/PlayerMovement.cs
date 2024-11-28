using Scripts.Data;
using Scripts.Data.Player;
using Scripts.Services.GridService;
using Scripts.Services.ManaService;
using Scripts.Services.SaveLoad;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scripts.Logic.Hero
{
    public class PlayerMovement : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private PlayerMana _playerMana;

        private GridTile currentTile;
        private bool isDragging = false;

        private IGridManager _gridManager;
        private IManaManager _manaManager;

        [Inject]
        public void Construct(IGridManager gridManager, IManaManager manaManager)
        {
            _gridManager = gridManager;
            _manaManager = manaManager;
        }

        private void Start() => currentTile = FindNearestTile(transform.position, _gridManager);

        private void OnMouseDown() => isDragging = true;

        private void OnMouseUp()
        {
            isDragging = false;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GridTile targetTile = FindNearestTile(new Vector2(mousePosition.x, mousePosition.y), _gridManager);

            if (targetTile != null)
            {
                if (_playerMana.GetCurrentMana() > 0 && IsNearBy(currentTile, targetTile))
                {
                    MoveToTile(targetTile);
                }     
                else
                    MoveToTile(currentTile);              
            }
        }

        private void Update()
        {         
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            }
        }

        public GridTile GetPlayerPosition() => _gridManager.GetTileAtPosition(transform.position);


        public void UpdateProgress(PlayerProgress progress) =>
            progress.worldData.positionOnlevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());


        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.worldData.positionOnlevel.level == CurrentLevel())
            {
                Vector3Data savedPosition = progress.worldData.positionOnlevel.position;
                if (savedPosition != null)
                    transform.position = savedPosition.AsUnityVector();
                currentTile = FindNearestTile(transform.position, _gridManager);
            }

        }

        private static string CurrentLevel() =>
           SceneManager.GetActiveScene().name;

        private GridTile FindNearestTile(Vector2 position, IGridManager _gridManager)
        {
            GridTile nearestTile = null;
            float nearestDistance = float.MaxValue;

            foreach (KeyValuePair<Vector2, GridTile> tilePair in _gridManager.GetTiles())
            {
                GridTile tile = tilePair.Value;
                float distance = Vector2.Distance(position, tile.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTile = tile;
                }
            }
            return nearestTile;
        }

        private void MoveToTile(GridTile targetTile)
        {
            if (currentTile == null && !IsNearBy(currentTile, targetTile) && _playerMana.GetCurrentMana() < 1)
                return;

            _manaManager.ConsumeMana(_playerMana, 1);

            transform.position = targetTile.transform.position;

            currentTile = targetTile;
        }

        private bool IsNearBy(GridTile tile1, GridTile tile2)
        {
            int distanceX = Mathf.Abs((int)tile1.transform.position.x - (int)tile2.transform.position.x);
            int distanceY = Mathf.Abs((int)tile1.transform.position.y - (int)tile2.transform.position.y);

            return distanceX <= 1 || distanceY <= 1;
        }       


    }
}