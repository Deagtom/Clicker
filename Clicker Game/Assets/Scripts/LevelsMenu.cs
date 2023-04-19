using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("volume");
    }
}
