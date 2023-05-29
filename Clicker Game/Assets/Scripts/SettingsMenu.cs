using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider;

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("balance", 0);
        PlayerPrefs.SetInt("factor", 1);
    }

    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
        PlayerPrefs.Save();
        AudioListener.volume = slider.value;
    }
}