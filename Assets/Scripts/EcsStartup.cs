using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;        
        IEcsSystems _systems;
        [SerializeField] private Text coinCounter, testValue, winScore, loseScore, targetText, lvlText;
        [SerializeField] private GameObject gameOverPanel, playerWonPanel, coinParticles, obstacleParticles;
        [SerializeField] private GameObject[] platforms;
        [SerializeField] private AnimationClip runAnim, jumpAnim, fallAnim;
        [SerializeField] private AudioSource coinSFX, obstacleSFX;
        [SerializeField] EcsUguiEmitter _uguiEmitter;
        void Start () {            
            var gameData = new GameData();
            //var timeComp = new TimeComponent();
            //anims
            gameData.runAnim = runAnim;
            gameData.jumpAnim = jumpAnim;
            gameData.fallAnim = fallAnim;
            //...
            gameData.coinCounter = coinCounter;
            gameData.winScore = winScore;
            gameData.loseScore = loseScore;
            gameData.targetText = targetText;
            gameData.lvlText = lvlText;
            gameData.platforms = platforms;
            gameData.testValue = testValue;
            gameData.coinParticles = coinParticles;
            gameData.obstacleParticles = obstacleParticles;
            gameData.coinSFX = coinSFX;
            gameData.obstacleSFX = obstacleSFX;
            gameData.gameOverPanel = gameOverPanel;
            gameData.playerWonPanel = playerWonPanel;
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world, gameData);
            _systems
                .Add(new InitCoins())
                .Add(new System_Coins())
                .Add(new InitPlayer())
                .Add(new SystemInputs())
                .Add(new System_PlayerMove())
                .Add(new SystemPlayerSnapBack())
                .Add(new InitPlatforms())
                .Add(new InitShared())
                .Add(new System_Timer())
                .Add(new UserSwipeInputSystem())
                
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())

                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
                .AddWorld(new EcsWorld(), Constants.Worlds.Events)
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .DelHere<HitComponent>()
                .Inject()
                .InjectUgui(_uguiEmitter, Constants.Worlds.Events)
                .Init();

            if (PlayerPrefs.GetInt("Lvl") < 1)
            {
                PlayerPrefs.SetInt("Lvl", 1);
            }
        }

        void Update () {
            // process systems here.
            _systems?.Run();
        }

        void FixedUpdate()
        {
            _systems.FixedRun();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }

        
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(0);
        }

        public void IncreaseLevel()
        {
            int lvl = PlayerPrefs.GetInt("Lvl") + 1;
            PlayerPrefs.SetInt("Lvl", lvl);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt("Lvl"));
        }
    }
}