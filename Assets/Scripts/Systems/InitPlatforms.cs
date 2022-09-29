using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class InitPlatforms : IEcsInitSystem {

        public void Init (IEcsSystems systems) {
            var world = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();            

            for (int i = 0; i < gameData.platformsAmount; i++)
            {
                var entity = world.NewEntity();
                ref var platformComp = ref world.GetPool<PlatformComponent>().Add(entity);
                platformComp.platform = GameObject.Instantiate(gameData.platforms[i]);
                platformComp.platform.transform.position = Vector3.forward * (i * 24.5f);
            }
        }
    }
}