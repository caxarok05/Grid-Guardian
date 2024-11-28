using Scripts.Data;
using Scripts.Data.Monsters;
using Scripts.Infrastructure.Factory;
using Scripts.Services.SaveLoad;
using Scripts.StaticData;
using UnityEngine;

namespace Scripts.Logic.Enemy
{
    public class EnemySpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId _enemyTypeId;
        public bool slain;

        public UniqueId uniqueId;

        private IGameFactory _gameFactory;
        private Attack _attack;

        public void Construct(MonsterTypeId type, IGameFactory factory)
        {
            _enemyTypeId = type;
            _gameFactory = factory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.killData.ClearedSpawners.Contains(uniqueId.Id))
                slain = true;
            else
               Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (slain)
                progress.killData.ClearedSpawners.Add(uniqueId.Id);

            if (_enemyTypeId == MonsterTypeId.BlueEnemy)
                progress.gridData.blueEnemies.Add(CreateNewEnemyData());
            if (_enemyTypeId == MonsterTypeId.GreenEnemy)
                progress.gridData.greenEnemies.Add(CreateNewEnemyData());

        }


        private async void Spawn()
        {
            GameObject monster = await _gameFactory.CreateMonster(_enemyTypeId, transform);
            _attack = monster.GetComponent<Attack>();
            _attack.Happened += Slay;
        }

        private void Slay()
        {
            if (_attack != null)
                _attack.Happened -= Slay;

            slain = true;
        }
        private EnemyData CreateNewEnemyData() => 
            new EnemyData() { id = uniqueId.Id, position = transform.position.AsVectorData(), Type = _enemyTypeId };
    }
}