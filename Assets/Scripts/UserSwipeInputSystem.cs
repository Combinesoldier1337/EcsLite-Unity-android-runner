using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;

namespace Client
{
    sealed class UserSwipeInputSystem : EcsUguiCallbackSystem
    {
        readonly EcsFilterInject<Inc<PlayerMoveComponent>> _filter = default;
        readonly EcsPoolInject<PlayerMoveComponent> _movePool = default;

        const float MinSwipeMagnitude = 0.2f;

        Vector2 _lastTouchPos = default;

        [Preserve]
        [EcsUguiDragStartEvent(Constants.Ui.TouchListener,Constants.Worlds.Events)]
        void OnDragStartListener(in EcsUguiDragStartEvent e)
        {
            _lastTouchPos = e.Position;
        }

        [Preserve]
        [EcsUguiDragMoveEvent(Constants.Ui.TouchListener, Constants.Worlds.Events)]
        void OnDragMoveListener(in EcsUguiDragMoveEvent e)
        {
            var swipe = Vector2.ClampMagnitude((e.Position - _lastTouchPos)/20, 1);

            foreach (int entity in _filter.Value)
            {
                ref var moveComp = ref _movePool.Value.Get(entity);
                moveComp.touchMoveVector = new Vector3(swipe.x/2, 0, Mathf.Clamp01(swipe.y));
            }
        }
        [Preserve]
        [EcsUguiDragEndEvent(Constants.Ui.TouchListener, Constants.Worlds.Events)]
        void onDragEndListener(in EcsUguiDragEndEvent e)
        {
            var swipe = Vector2.ClampMagnitude(e.Position - _lastTouchPos, 1);

            foreach (int entity in _filter.Value)
            {
                ref var moveComp = ref _movePool.Value.Get(entity);
                moveComp.touchMoveVector = Vector3.zero;
            }
        }
    }
}