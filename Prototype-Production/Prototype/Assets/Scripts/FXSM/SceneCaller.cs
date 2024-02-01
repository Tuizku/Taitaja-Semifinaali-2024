using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCaller : MonoBehaviour
{
    /// <summary>
    /// Sets the next transition
    /// </summary>
    /// <param name="fx">Transition effect scriptable object</param>
    public void SetFX(Transition fx)
    {
        FXSceneManager.current = fx;
    }
    /// <summary>
    /// Loads a scene while using a transition
    /// </summary>
    /// <param name="sceneBuildIndex">Scene index</param>
    public void FXLoadScene(int sceneBuildIndex)
    {
        StartCoroutine(FXSceneManager.FXLoadSceneIE(sceneBuildIndex, FXSceneManager.current));
    }
    /// <summary>
    /// Loads the next scene using a transition
    /// </summary>
    public void FXLoadNextScene()
    {
        StartCoroutine(FXSceneManager.FXLoadSceneIE((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings, FXSceneManager.current));
    }
    /// <summary>
    /// Exits the game using the provided transition
    /// </summary>
    public void FXExit()
    {
        StartCoroutine(FXSceneManager.FXExitIE(FXSceneManager.current));
    }
    /// <summary>
    /// Resets the current scene. Uses transition
    /// </summary>
    public void FXReset()
    {
        StartCoroutine(FXSceneManager.FXLoadSceneIE(SceneManager.GetActiveScene().buildIndex, FXSceneManager.current));
    }
}
