using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using UnityEngine;

namespace Client {
    sealed class System_CoinSnap : IEcsFixedRunSystems {
        readonly EcsFilterInject<Inc<SlideComponent>> _playerFilter = default;
        readonly EcsFilterInject<Inc<CoinPosComponent>> _coinPosFilter = default;

        readonly EcsPoolInject<SlideComponent> _playerPool = default;
        readonly EcsPoolInject<CoinPosComponent> _coinPosPool = default;
        public void FixedRun (IEcsSystems systems) {
            foreach (int entity in _playerFilter.Value)
            {
                ref SlideComponent playerComp = ref _playerPool.Value.Get(entity);
                if (playerComp.transform.position.z > 12)
                {
                    Debug.Log("player pos > 12");
                }
            }
        }
    }
}