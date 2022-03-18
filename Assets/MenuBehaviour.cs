using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject tela;
    public void Change(string nome)
    {
        StartCoroutine(AsyncLoad(nome));
    }
    IEnumerator AsyncLoad(string nome)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nome);
        tela.SetActive(true);
        while(!ao.isDone)
        {
            slider.value = ao.progress;
            yield return null;
        }
    }
}
