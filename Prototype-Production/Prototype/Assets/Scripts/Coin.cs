using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    public void Add()
    {
        DataManager.ChangeCoins(value);
    }
}
