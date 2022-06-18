using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static int currentCoins;
    public int startMoney = 400;
    
    void Start()
    {
        currentCoins = startMoney;
    }

    void Update()
    {
        
    }
    public static void Increase(int amount)
    {
        currentCoins += amount;
    }
}
