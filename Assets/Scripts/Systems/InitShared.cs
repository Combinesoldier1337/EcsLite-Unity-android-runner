using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class InitShared : IEcsInitSystem {
        public void Init (IEcsSystems systems) {
            // Shared entity to apply multiple components

            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var time = ref world.GetPool<TimeComponent>().Add(entity);
            time.Time = Time.time;
            time.UnscaledTime = Time.unscaledTime;
            time.UnscaledDeltaTime = Time.unscaledDeltaTime;
            time.DeltaTime = Time.deltaTime;
        }
    }
}