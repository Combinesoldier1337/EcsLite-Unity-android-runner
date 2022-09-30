using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WorldCurver))]
public class main_menu : MonoBehaviour
{
    [SerializeField] private float curveClampAmount = -0.0026f;
    [SerializeField] private bool wave = true; 
    WorldCurver worldCurver;
    float n = 0;


    // Start is called before the first frame update
    void Start()
    {
        worldCurver = GetComponent<WorldCurver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wave)
        {
            n = n > 6.3f ? 0 : n + Time.fixedDeltaTime;
            worldCurver.curveStrength = Mathf.Sin(n) * curveClampAmount;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetProgressData()
    {
        PlayerPrefs.SetInt("Lvl", 1);
        PlayerPrefs.Save();
    }
}
