using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class System_Timer : IEcsFixedRunSystems {

        readonly EcsFilterInject<Inc<TimeComponent>> _filer = default;
        readonly EcsPoolInject<TimeComponent> _getTimerPool = default;

        public void FixedRun (IEcsSystems systems) {
            var gameData = systems.GetShared<GameData>();
            foreach (var entity in _filer.Value)
            {
                ref var time = ref _getTimerPool.Value.Get(entity);
                time.Time = Time.time;
                time.UnscaledTime = Time.unscaledTime;
                time.UnscaledDeltaTime = Time.unscaledDeltaTime;
                time.DeltaTime = Time.deltaTime;
            }                  
        }
    }
}