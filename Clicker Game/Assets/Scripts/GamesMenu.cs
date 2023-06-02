using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesMenu : MonoBehaviour
{
    public void ToMenu() => SceneManager.LoadScene(0);

    public void ToGameOne() => SceneManager.LoadScene(4);

    public void ToGameTwo() => SceneManager.LoadScene(5);

    public void ToGameThree() => SceneManager.LoadScene(6);
}