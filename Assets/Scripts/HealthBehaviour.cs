using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthBehaviour : MonoBehaviour
{
    [Header("Health Settings")]
    public static int hp;
    public int totalDeVidas = 10;

    [Header("GameOver Settings")]
    public string nomeCena;
    [Header("UI Settings")]
    public TextMeshProUGUI TextoVida;

    void Start()
    {
        hp = totalDeVidas;
    }
    void Update()
    {
        if (hp <= 0)
            EndGame();
        TextoVida.text = hp.ToString() + " vidas";
    }

    void EndGame () 
    {
        Debug.Log("Fim de jogo!");
        SceneManager.LoadSceneAsync(nomeCena);
    }

}
