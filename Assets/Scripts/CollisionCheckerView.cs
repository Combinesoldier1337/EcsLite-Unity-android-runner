using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class CollisionCheckerView : MonoBehaviour
    {
        public EcsWorld world { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.CoinTag))
            {
                // instantly destroy coin to avoid multiple OnTriggerEnter() calls.
                other.gameObject.SetActive(false);
            }
            //var hit = ecsWorld.NewEntity();
            var hit = world.NewEntity();
            var hitPool = world.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.first = transform.root.gameObject; // player
            hitComponent.other = other.gameObject;          // coin
            
        }
    }
}