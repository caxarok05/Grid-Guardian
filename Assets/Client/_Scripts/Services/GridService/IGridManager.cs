using Scripts.Infrastructure.Factory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Services.GridService
{
    public interface IGridManager
    {
        Task GenerateGrid(int _width, int _height, Transform _initialPoint, IGameFactory _gameFactory);
        GridTile GetTileAtPosition(Vector2 pos);
        Dictionary<Vector2, GridTile> GetTiles();
        void ChangeAvailability(Vector2 path, bool isFree);

        event Action GridCreated;
        bool IsGridCreated { get; set; }
    }
}