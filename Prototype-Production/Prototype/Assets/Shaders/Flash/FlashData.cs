using UnityEngine;

// The data object for damage flashes

[CreateAssetMenu(fileName = "New Shake", menuName = "Polishing/Flash")]
public class FlashData : ScriptableObject
{
    public string shaderProperty; // The shader property to alter
    public Color color; // The tint of the flash
    public float duration; // The duration of the flash
    public AnimationCurve curve; // The curve the flash power follows
}