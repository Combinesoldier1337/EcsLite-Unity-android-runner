using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class InitPlatforms : IEcsInitSystem {

        public void Init (IEcsSystems systems) {
            var world = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();
            int lvl = PlayerPrefs.GetInt("Lvl");
            gameData.lvlText.text = "Level " + lvl;
            gameData.CoinsAmountTarget = 50 + (50 * lvl);
            gameData.targetText.text = "Collect " + gameData.CoinsAmountTarget + " coins";

            System.Array.Sort(gameData.platforms, RandomSort);

            for (int i = 0; i < gameData.platformsAmount; i++)
            {
                var entity = world.NewEntity();
                ref var platformComp = ref world.GetPool<PlatformComponent>().Add(entity);
                platformComp.platform = GameObject.Instantiate(gameData.platforms[i]);
                platformComp.platform.transform.position = Vector3.forward * (i * 24.5f);
            }
        }

        int RandomSort(GameObject a, GameObject b)
        {
            return Random.Range(-1, 2);

        }
    }
}