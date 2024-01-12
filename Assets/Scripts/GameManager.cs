using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isRunning;
    //private int WelcomeScene = 0;
    private int PauseScene = 1;
    private int GameOverScene = 2;
    //private int Level01 = 3;
    private int Level02 = 4;
    /*public enum SceneName
    {
        WelcomeScene,
        PauseScene,
        GameOverScene,
        Level01,
        Level02
    }
    public SceneName sceneName;*/

    // Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isRunning)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (isRunning)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void LoadScene(int target, LoadSceneMode loadSceneMode)
    {
        Scene targetScene = SceneManager.GetSceneByBuildIndex(target);

        if (!targetScene.isLoaded)
        {
            SceneManager.LoadScene(target, loadSceneMode);
        }

        if (!isRunning)
        {
            isRunning = true;
        }
    }

    public void UnloadScene(int target)
    {
        Scene targetScene = SceneManager.GetSceneByBuildIndex(target);

        if (targetScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(target);
        }

        if (!isRunning)
        {
            isRunning = true;
        }
    }

    /*public void StartGame()
    {
        isRunning = true;
        LoadScene(Level01, LoadSceneMode.Single);
    }*/

    private void PauseGame()
    {
        isRunning = false;
        LoadScene(PauseScene, LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        isRunning = true;
        UnloadScene(PauseScene);
    }

    /*public void RestartGame()
    {
        isRunning = true;
        LoadScene(WelcomeScene, LoadSceneMode.Single);
    }*/
    
    public void GameOver()
    {
        isRunning = false;
        LoadScene(GameOverScene, LoadSceneMode.Additive);
    }

    public void NextScene()
    {
        isRunning = true;
        LoadScene(Level02, LoadSceneMode.Single);
    }
}
