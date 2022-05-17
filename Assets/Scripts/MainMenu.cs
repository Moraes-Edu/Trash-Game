using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void PlayGame(string nome)
    {
        StartCoroutine(AsyncLoad(nome));
    }

    public void QuitGame()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }
    IEnumerator AsyncLoad(string nome)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nome);
        while(!ao.isDone)
        {
            slider.value = ao.progress;
            yield return null;
        }
    }
}
