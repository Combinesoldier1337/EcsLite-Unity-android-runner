using System.Collections;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class InitCoins : IEcsPreInitSystem {
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            var coins = GameObject.FindGameObjectsWithTag("Collectable");
            var gameData = systems.GetShared<GameData>();

            var world = systems.GetWorld();
            

            gameData.CoinsAmount = 0;
            gameData.platformsAmount = gameData.platforms.Length;
            gameData.CoinsAmountTarget = 100;

            foreach (GameObject coin in coins)
            {
                var entity = world.NewEntity();
                ref var coinComp = ref world.GetPool<CoinPosComponent>().Add(entity);
                coinComp.coinGO = coin;
            }           
        }        
    }
}