using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    [Header("Sound")]
    [SerializeField] Sprite soundOn,soundOff;
    bool sound;
    SoundController soundController;
    private void Start()
    {
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }
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
    public void OnClick(Button s)
    {
        s.GetComponent<Image>().sprite = sound ? soundOn : soundOff;
        sound = !sound;
        soundController.Mute(sound);
    }
    public void OnChanged(Slider s)
    {
        soundController.ChangeVolume(s.value);
    }
}
