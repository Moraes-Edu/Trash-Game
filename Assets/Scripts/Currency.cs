using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] Text currencyText;
    static Text textCurrency;
    public static int currentCoins;
    public int startMoney = 400;
    
    void Start()
    {
        currentCoins = startMoney;
        textCurrency = currencyText;
        OnChange();
    }
    public static void Increase(int amount)
    {
        currentCoins += amount;
        OnChange();
    }
    public static bool Decrease(int amount)
    {
        if (currentCoins < amount)
        {
            return false;
        }
        else
        {
            currentCoins -= amount;
            OnChange();
            return true;
        }
    }
    private static void OnChange()
    {
        textCurrency.text = $"Coins: {currentCoins}";
    }
}
