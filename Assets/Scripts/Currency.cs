using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static int CurrentCoins { get; private set; }
    
    void Start()
    {
        CurrentCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void Increase(int amount)
    {
        CurrentCoins += amount;
    }
}
