using Scripts.Infrastructure.AssetManagement;
using Scripts.Infrastructure.Factory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Services.GridService
{
    public class GridManager : IGridManager
    {
        public bool IsGridCreated { get; set; } = false;
        public event Action GridCreated;
        
        private Dictionary<Vector2, GridTile> _tiles;

        public async Task GenerateGrid(int _width, int _height, Transform _initialPoint, IGameFactory _gameFactory)
        {
            _tiles = new Dictionary<Vector2, GridTile>();

            for (int x = (int)_initialPoint.position.x; x < (int)_initialPoint.position.x + _width; x++)
            {
                for (int y = (int)_initialPoint.position.y; y < (int)_initialPoint.position.y + _height; y++)
                {
                    GameObject Tile = await _gameFactory.CreateEntity(AssetAdress.GridTilePath, new Vector3(x, y));
                    GridTile spawnedTile = Tile.GetComponent<GridTile>();
                    spawnedTile.name = $"Tile {x} {y}";

                    bool isOffset = x % 2 == 0 && y % 2 != 0 || x % 2 != 0 && y % 2 == 0;
                    spawnedTile.Init(isOffset);


                    _tiles[new Vector2(x, y)] = spawnedTile;
                }
            }
            _initialPoint.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

            GridCreated?.Invoke();
            IsGridCreated = true;
        }

        public GridTile GetTileAtPosition(Vector2 pos)
        {
            if (_tiles.TryGetValue(pos, out GridTile tile)) return tile;
            return null;
        }

        public Dictionary<Vector2, GridTile> GetTiles() => _tiles;

        public void ChangeAvailability(Vector2 path, bool isFree) => _tiles[path]._isFree = isFree;
    }
}