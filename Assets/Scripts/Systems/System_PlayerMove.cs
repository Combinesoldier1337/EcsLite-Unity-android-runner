using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using UnityEngine;

namespace Client {
    sealed class System_PlayerMove : IEcsFixedRunSystems {
        //readonly EcsWorldInject _world = default;
        readonly EcsFilterInject<Inc<PlayerMoveComponent>> _filter = default;

        readonly EcsPoolInject<PlayerMoveComponent> _movePool = default;
        readonly EcsPoolInject<ViewComponent> _viewPool = default;
        readonly EcsPoolInject<InputComponent> _inputPool = default;
        readonly EcsPoolInject<TimeComponent> _timePool = default;
        
        public void FixedRun (IEcsSystems systems) {

            float blend = 0;
            var gameData = systems.GetShared<GameData>();

            foreach (int entity in _filter.Value)
            {
                PlayerMoveComponent moveComp = _movePool.Value.Get(entity);
                ViewComponent viewComp = _viewPool.Value.Get(entity);
                TimeComponent timeComp = _timePool.Value.Get(entity);

                Vector3 movePos = viewComp.transform.position + (moveComp.moveVector * moveComp.speed * timeComp.DeltaTime);
                viewComp.rigidBody.MovePosition(new Vector3(Mathf.Clamp(movePos.x, -1, 1), movePos.y, movePos.z));
                viewComp.transform.GetChild(0).rotation = Quaternion.Euler(moveComp.moveVector);
                Animator animator = viewComp.transform.GetChild(0).GetComponent<Animator>();
                blend = animator.GetFloat("Blend");
                if (blend > 0.5f)
                {
                    blend -= timeComp.DeltaTime;
                    animator.SetFloat("Blend", blend);
                    animator.Play("RollForward");
                }
                else
                {
                    animator.SetFloat("Blend", (timeComp.DeltaTime * 25) * Mathf.Clamp01(moveComp.moveVector.magnitude));
                }
                
            }
        }
    }
}