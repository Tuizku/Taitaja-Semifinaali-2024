using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FXSceneManager : MonoBehaviour
{
    // VARIABLES
    public static Transition current;
    public static bool inProgress = false;
    public static FXSceneManager self;

    private void Start()
    {
        self = this;

        // If coming from a transition apply the ending
        if (current != null)
            StartCoroutine(FXSceneLoadedIE(current));
    }

    // The IEnumerators the previous methods call
    public static IEnumerator FXLoadSceneIE(int sceneBuildIndex, Transition fx)
    {
        if (inProgress) yield break;
        inProgress = true;

        yield return fx.OnStartTransition();

        SceneManager.LoadSceneAsync(sceneBuildIndex);
    }
    public static IEnumerator FXSceneLoadedIE(Transition fx)
    {
        yield return fx.OnEndTransition();

        current = null;
        inProgress = false;
    }
    public static IEnumerator FXExitIE(Transition fx)
    {
        if (inProgress) yield break;
        inProgress = true;

        yield return fx.OnStartTransition();

        Application.Quit();
    }

    // Renders the transition
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (current == null) {
            Graphics.Blit(source, destination);
            return;
        }
        current.Apply(source, destination);
    }
}