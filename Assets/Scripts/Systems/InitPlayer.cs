using UnityEngine;
using Leopotam.EcsLite;

namespace Client
{
    sealed class InitPlayer : IEcsInitSystem    {
        public void Init(IEcsSystems systems)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            var child  = go.transform.GetChild(0);

            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var timeComp = ref world.GetPool<TimeComponent>().Add(entity);
            timeComp.UnscaledTime = Time.unscaledTime;
            timeComp.UnscaledDeltaTime = Time.unscaledDeltaTime;
            timeComp.DeltaTime = Time.deltaTime;
            timeComp.Time = Time.time;

            ref var moveComp = ref world.GetPool<PlayerMoveComponent>().Add(entity);
            moveComp.speed = 8;
            moveComp.moveVector = Vector3.zero;
            moveComp.touchMoveVector = Vector3.zero;

            ref var viewComp = ref world.GetPool<ViewComponent>().Add(entity);
            viewComp.transform = go.transform;
            viewComp.rigidBody = go.GetComponent<Rigidbody>();

            ref var inputComp = ref world.GetPool<InputComponent>().Add(entity);
            inputComp.pos = Vector3.zero;

            ref var slideComp = ref world.GetPool<SlideComponent>().Add(entity);
            slideComp.transform = child.transform;     
            
            
            child.GetComponent<CollisionCheckerView>().world = world;
        }
    }
}

