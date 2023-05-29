using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesMenu : MonoBehaviour
{
    [SerializeField] private int attemptsForRoulette;
    [SerializeField] private int attemptsForFlappyCoin;

    private void Start()
    {
        attemptsForRoulette = PlayerPrefs.HasKey("attempts for roulette") ? PlayerPrefs.GetInt("attempts for roulette") : 0;
        attemptsForFlappyCoin = PlayerPrefs.HasKey("attempts for flappy coin") ? PlayerPrefs.GetInt("attempts for flappy coin") : 0;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToGameOne()
    {
        if (attemptsForRoulette >= 0)
        {
            attemptsForRoulette--;
            PlayerPrefs.SetInt("attempts for roulette", attemptsForRoulette);
            SceneManager.LoadScene(4);
        }
    }

    public void ToGameTwo()
    {
        if (attemptsForRoulette >= 0)
        {
            attemptsForFlappyCoin--;
            PlayerPrefs.SetInt("attempts for flappy coin", attemptsForFlappyCoin);
            SceneManager.LoadScene(5);
        }
    }

    public void ToGameThree()
    {
        SceneManager.LoadScene(6);
    }
}