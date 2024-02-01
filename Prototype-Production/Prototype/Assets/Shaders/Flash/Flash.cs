using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private Coroutine flashCoroutine; // Keep track of flash coroutines in order to stop them before starting new ones
    /// <summary>
    /// Applies a short effect onto the shader. Meant mostly for damage flash effects
    /// </summary>
    /// <param name="target">The object to apply the effect onto</param>
    /// <param name="shaderProperty">The property to adjust</param>
    /// <param name="color">The color of the flash</param>
    /// <param name="duration">The length of the flash</param>
    /// <param name="curve">The power curve the flash follows</param>
    private IEnumerator DamageFlashEnumerator(GameObject target, string shaderProperty, Color color, float duration, AnimationCurve curve)
    {
        Renderer render = target.GetComponent<Renderer>(); // Get the object's renderer
        float waited = 0.0f;

        while (waited < duration) // Loop until waited for duration
        {
            waited += Time.deltaTime; // Progress time

            // Apply the flash effect by lerping the alpha of the flash tint
            render.material.SetColor(
                shaderProperty,
                new Color(color.r, color.g, color.b, Mathf.Lerp(color.a, 0.0f, waited / duration))
            );
            yield return 0; // Wait one frame
        }
    }
    /// <summary>
    /// A version of the damage flash function used to apply the flash from the Unity Editor
    /// </summary>
    /// <param name="target"></param>
    public void DamageFlash(FlashData data)
    {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine); // Stop previous coroutine
        if (data == null) { Debug.LogWarning("Damage flash had no property set!"); } // Warn if flash properties not set
        flashCoroutine = StartCoroutine(DamageFlashEnumerator(gameObject, data.shaderProperty, data.color, data.duration, data.curve)); // Calls the original function
    }
}
