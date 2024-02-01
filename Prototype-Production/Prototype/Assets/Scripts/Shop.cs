using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    static int SwordLevel = 0;

    [SerializeField] private List<Sword> swords;

    public UnityEvent<Sprite> UpdateSwordSprite;
    public UnityEvent<string> UpdateSwordCost;
    public UnityEvent<Sword> OnUpdateSword;

    void Start()
    {
        //DataManager.ChangeCoins(300);
    }

    public void BuyNextSword()
    {
        print("coins now" + DataManager.Data.Coins);
        Sword nextSword = swords[SwordLevel + 1];
        if (DataManager.Data.Coins >= nextSword.Cost)
        {
            SwordLevel++;
            DataManager.ChangeCoins(-nextSword.Cost);
            UpdateSwordSprite.Invoke(nextSword.Sprite);
            UpdateSwordCost.Invoke(nextSword.Cost.ToString());
            OnUpdateSword.Invoke(nextSword);
        }
    }
}
