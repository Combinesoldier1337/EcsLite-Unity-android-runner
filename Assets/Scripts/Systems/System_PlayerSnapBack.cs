using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class SystemPlayerSnapBack : IEcsRunSystem {

        readonly EcsFilterInject<Inc<PlayerMoveComponent>> _filter = default;
        readonly EcsFilterInject<Inc<PlatformComponent>> _filter_platforms = default;
        readonly EcsPoolInject<ViewComponent> _viewPool = default;
        readonly EcsPoolInject<PlatformComponent> _platformsPool = default;
        int currentFirstPlatform = 1;
        public void Run (IEcsSystems systems) {
            var gameData = systems.GetShared<GameData>();
            foreach (int entity in _filter.Value)
            {
                ViewComponent viewComp = _viewPool.Value.Get(entity);
                viewComp.transform.position = viewComp.transform.position.z >= 15.5f ? new Vector3(viewComp.transform.position.x, viewComp.transform.position.y, Snap()) : viewComp.transform.position;
            }

            int Snap()
            {
                foreach (int entity in _filter_platforms.Value)
                {
                    PlatformComponent platformComp = _platformsPool.Value.Get(entity);
                    platformComp.platform.transform.position -= Vector3.forward * 24.5f;                    
                }
                PlatformComponent firstPlatformComp = _platformsPool.Value.Get(currentFirstPlatform);
                firstPlatformComp.platform.transform.position += Vector3.forward * 24.5f * gameData.platformsAmount;
                foreach(Transform child in firstPlatformComp.platform.transform.GetChild(0))
                {
                    child.gameObject.SetActive(true);
                }

                currentFirstPlatform = currentFirstPlatform >= gameData.platformsAmount ? 1 : currentFirstPlatform + 1;

                return -9;
            }
        }        
    }
}