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

    [Header("GameOver Settings")]
    [SerializeField] string nomeCena;
    [Header("UI Settings")]
    [SerializeField] Text TextoVida;

    void Start()
    {
        hp = totalDeVidas;
    }
    void Update()
    {
        if (hp <= 0)
            EndGame();
        TextoVida.text = hp.ToString() + " vidas restante";
    }

    void EndGame () 
    {
        Debug.Log("Fim de jogo!");
        SceneManager.LoadSceneAsync(nomeCena);
    }
    public static void Decrease(int n)
    {
        hp -= n;
    }
    public static void Increase(int n)
    {
        hp += n;
    }

}
