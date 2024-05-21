using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Sword", order = 1)]
public class Sword : ScriptableObject
{
    public Sprite Sprite;
    public int Damage;
    public int Cost;
}
