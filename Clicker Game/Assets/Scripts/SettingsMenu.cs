using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
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

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("volume");
    }
}
