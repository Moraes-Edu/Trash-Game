using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthBehaviour : MonoBehaviour
{
    [Header("Health Settings")]
    private static int hp;
    [SerializeField] int totalDeVidas = 10;
    [Header("UI Settings")]
    [SerializeField] Text TextoVida;
    static Text tv;

    void Start()
    {
        hp = totalDeVidas;
        tv = TextoVida;
    }
    static void EndGame () 
    {
        Debug.Log("Fim de jogo!");
        SceneManager.LoadSceneAsync("GameOver");
    }
    public static void Decrease(int n)
    {
        hp -= n;
        if (hp <= 0)
            EndGame();
        tv.text = $"Lifes: {hp}";
    }

}
