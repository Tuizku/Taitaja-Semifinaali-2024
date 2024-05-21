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
        Sword nextSword = swords[SwordLevel + 1];
        UpdateSwordSprite.Invoke(nextSword.Sprite);
        UpdateSwordCost.Invoke(nextSword.Cost.ToString());
        OnUpdateSword.Invoke(swords[SwordLevel]);
    }

    public void BuyNextSword()
    {
        print("coins now: " + DataManager.Data.Coins);
        print("sword level: " + SwordLevel);
        print($"{SwordLevel + 2} >= {swords.Count}");

        if (SwordLevel + 2 >= swords.Count)
        {
            UpdateSwordCost.Invoke("Max");
            return;
        }

        Sword nextSword = swords[SwordLevel + 1];
        if (DataManager.Data.Coins >= nextSword.Cost)
        {
            DataManager.ChangeCoins(-nextSword.Cost);
            OnUpdateSword.Invoke(nextSword);

            SwordLevel++;
            nextSword = swords[SwordLevel + 1];
            
            UpdateSwordSprite.Invoke(nextSword.Sprite);
            UpdateSwordCost.Invoke(nextSword.Cost.ToString());
            
        }
    }
}
