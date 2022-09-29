using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class SystemInputs : IEcsRunSystem {

        readonly EcsFilterInject<Inc<PlayerMoveComponent>> _filter = default;
        readonly EcsPoolInject<PlayerMoveComponent> _movePool = default;
        public void Run (IEcsSystems systems) {

            foreach (int entity in _filter.Value)
            {
                ref var moveComp = ref _movePool.Value.Get(entity);
                //PlayerMoveComponent moveComp = _movePool.Value.Get(entity);
                Vector3 keyboardInput = new Vector3(Input.GetAxis(Constants.Input.HorizontalAxis), 0, Mathf.Clamp01(Input.GetAxis(Constants.Input.VerticalAxis)));
                moveComp.moveVector = keyboardInput + moveComp.touchMoveVector;                
            }
        }
    }
}