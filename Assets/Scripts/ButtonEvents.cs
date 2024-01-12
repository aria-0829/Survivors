using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public Scenes.SceneName sceneName;
    public enum LoadMode
    {
        Single,
        Additive
    }
    public LoadMode loadMode;

    private void OnGUI()
    {
        sceneName = (Scenes.SceneName)EditorGUILayout.EnumPopup("Scene Name", sceneName);
        loadMode = (LoadMode)EditorGUILayout.EnumPopup("Load Mode", loadMode);
    }

    public void Load()
    {
        LoadSceneMode loadSceneMode = (loadMode == LoadMode.Single) ? LoadSceneMode.Single : LoadSceneMode.Additive;
        
        GameManager.Instance.LoadScene((int)sceneName, loadSceneMode);
    }

    public void Unload()
    {
        GameManager.Instance.UnloadScene((int)sceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
