using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Client
{
    public class GameData
    {
        public Text coinCounter, winScore, loseScore;
        public Text testValue;
        public GameObject[] platforms;
        public GameObject playerWonPanel, gameOverPanel, coinParticles, obstacleParticles;
        public AudioSource coinSFX, obstacleSFX;
        public SceneService sceneService;

        public AnimationClip runAnim, jumpAnim, fallAnim;

        public int CoinsAmount, CoinsAmountTarget;
        public int platformsAmount;
    }
}