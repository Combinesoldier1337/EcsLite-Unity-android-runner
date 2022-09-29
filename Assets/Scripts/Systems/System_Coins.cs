using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
namespace Client
{
    sealed class System_Coins : IEcsRunSystem {

        readonly EcsFilterInject<Inc<PlayerMoveComponent>> _filter = default;
        readonly EcsPoolInject<PlayerMoveComponent> _getPlayerHitPool = default;  
        public void Run(IEcsSystems systems) {

            var gameData = systems.GetShared<GameData>();
            var hitFilter = systems.GetWorld().Filter<HitComponent>().End();
            var hitPool = systems.GetWorld().GetPool<HitComponent>();
            var viewPool = systems.GetWorld().GetPool<ViewComponent>();

            foreach (var hitEntity in hitFilter)
            {                
                ref var hitComponent = ref hitPool.Get(hitEntity);
                foreach (var playerEntity in _filter.Value)
                {
                    ref var coinComp = ref _getPlayerHitPool.Value.Get(playerEntity);
                    if (hitComponent.other.CompareTag(Constants.Tags.CoinTag))
                    {
                        AddCoinsAmount(1);
                        gameData.coinParticles.transform.position = hitComponent.other.transform.position;
                        gameData.coinParticles.GetComponent<ParticleSystem>().Play();

                        gameData.coinSFX.Play();
                    }

                    if (hitComponent.other.CompareTag(Constants.Tags.HarmTag))
                    {
                        AddCoinsAmount(-5);
                        gameData.obstacleParticles.transform.position = hitComponent.other.transform.position + Vector3.up;
                        gameData.obstacleParticles.GetComponent<ParticleSystem>().Play();

                        gameData.obstacleSFX.Play();

                        ref var playerComp = ref viewPool.Get(playerEntity);
                        playerComp.transform.GetChild(0).GetComponent<Animator>().SetFloat("Blend", 1);
                    }
                    if (gameData.CoinsAmount < 0)
                    {
                        AddCoinsAmount(1);
                        SetActivePanel(gameData.gameOverPanel);

                    }
                    else if (gameData.CoinsAmount >= gameData.CoinsAmountTarget)
                    {
                        SetActivePanel(gameData.playerWonPanel);
                    }
                }
            }
            void AddCoinsAmount(int coins)
            {
                gameData.CoinsAmount += coins;
                gameData.coinCounter.text = gameData.CoinsAmount.ToString();
            }
            void SetActivePanel (GameObject panel)
            {
                panel.SetActive(true);
                gameData.winScore.text = "score: \n" + gameData.CoinsAmount + "/" + gameData.CoinsAmountTarget;
            }

        }

    }
}