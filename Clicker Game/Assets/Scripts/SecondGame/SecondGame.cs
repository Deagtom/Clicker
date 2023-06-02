using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SecondGame : MonoBehaviour
{
    public GameObject pipes;
    public static float spawnTime = 2f;

    [SerializeField] private int _attemptsForMiniGames;
    [SerializeField] private Text _attemptsText;

    public GameObject startButton;
    public GameObject menuButton;
    public GameObject attemptsText;
    public GameObject nameGame;
    public GameObject score;

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void Update() => _attemptsText.text = "Attempts: " + _attemptsForMiniGames;

    private void Start()
    {
        Time.timeScale = 0f;
        _attemptsForMiniGames = PlayerPrefs.HasKey("attempts for mini games") ? PlayerPrefs.GetInt("attempts for mini games") : 0;
        StartCoroutine(PipeSpawn());
    }

    private IEnumerator PipeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject newPipes = Instantiate(pipes, new Vector3(2, Random.Range(-1.75f, 1.75f), 0), Quaternion.identity);
            Destroy(newPipes, 7f);
        }
    }

    public void StartLevel()
    {
        if (_attemptsForMiniGames > 0)
        {
            _attemptsForMiniGames--;
            PlayerPrefs.SetInt("attempts for mini games", _attemptsForMiniGames);
            Destroy(startButton);
            Destroy(nameGame);
            Time.timeScale = 1f;
            score.SetActive(true);
            menuButton.SetActive(false);
            attemptsText.SetActive(false);
        }
    }
}