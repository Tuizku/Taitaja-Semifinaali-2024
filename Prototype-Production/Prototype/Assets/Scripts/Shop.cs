using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    static int SwordLevel = 0;

    [SerializeField] private List<Sword> swords;

    public UnityEvent<Sprite> UpdateSword;

    public void BuyNextSword()
    {

    }
}
