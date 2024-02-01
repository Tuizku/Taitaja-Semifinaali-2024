using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Basic Transition", menuName = "Transitions/Basic Transition")]
public class Transition : ScriptableObject
{
    // The transition shader
    public Shader shader;
    // The duration of the transition
    public float duration = 1f;
    // The speed curve of the animation
    [SerializeField] private AnimationCurve curve;
    // The passed time
    [System.NonSerialized] public float waited = 0f;
    // The transition material
    [System.NonSerialized] private Material sMat;

    public virtual IEnumerator OnStartTransition()
    {
        waited = 0f;

        while (waited < duration)
        {
            waited += Time.deltaTime * 2;
            yield return 0;
        }
    }
    public virtual IEnumerator OnEndTransition()
    {
        while (waited > 0)
        {
            waited -= Time.deltaTime * 2;
            yield return 0;
        }

        Reset();
    }
    /// <summary>
    /// Sets the shaders properties, override this to send more data to the transition shader
    /// </summary>
    /// <param name="source">The source rendertexture</param>
    /// <param name="destination">The destination rendertexture</param>
    /// <param name="mat">The transition shader material</param>
    public virtual void SetShaderVariables(RenderTexture source, RenderTexture destination, Material mat)
    {
        mat.SetTexture("_MainTex", source);
        mat.SetFloat("_Prog", curve.Evaluate(waited / duration));
    }
    /// <summary>
    /// Draws the transition effect on screen.
    /// </summary>
    /// <param name="source">The source rendertexture</param>
    /// <param name="destination">The destination rendertexture</param>
    public virtual void Apply(RenderTexture source, RenderTexture destination)
    {
        if (sMat == null) sMat = new Material(shader);

        SetShaderVariables(source, destination, sMat);
        Graphics.Blit(source, destination, sMat);
    }
    public virtual void Reset() { }
}