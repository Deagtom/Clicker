using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdGame : MonoBehaviour
{
    [SerializeField] private int _attemptsForMiniGames;
    [SerializeField] private Text _attemptsText;

    public GameObject startButton;
    public GameObject menuButton;
    public GameObject attemptsText;
    public GameObject right;
    public GameObject left;
    public GameObject score;
    public GameObject nameGame;
    public GameObject[] plates;

    public GameObject stick;
    public GameObject ball;

    private bool _rightMove = false;
    private bool _leftMove = false;

    public void RightDown() => _rightMove = true;

    public void RightUp() => _rightMove = false;

    public void LeftDown() => _leftMove = true;

    public void LeftUp() => _leftMove = false;

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void Update() => _attemptsText.text = "Attempts: " + _attemptsForMiniGames;

    private void Start()
    {
        Time.timeScale = 0f;
        _attemptsForMiniGames = PlayerPrefs.HasKey("attempts for mini games") ? PlayerPrefs.GetInt("attempts for mini games") : 0;
    }

    private void FixedUpdate()
    {
        if (_rightMove)
            stick.transform.Translate(new Vector2(5, 0) * Time.deltaTime);
        else if (_leftMove)
            stick.transform.Translate(new Vector2(-5, 0) * Time.deltaTime);
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
            menuButton.SetActive(false);
            attemptsText.SetActive(false);
            stick.SetActive(true);
            ball.SetActive(true);
            right.SetActive(true);
            left.SetActive(true);
            score.SetActive(true);
            plates[Random.Range(0, 3)].SetActive(true);
        }
    }
}