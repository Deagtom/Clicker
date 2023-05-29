using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] public Text scoreText;

    private void Start()
    {
        _score = 0;
    }

    private void FixedUpdate()
    {
        scoreText.text = _score.ToString();
        if (_score == 5)
        {
            PipesMove.speed = 2.4f;
            SecondGame.spawnTime = 1.8f;
        }
        if (_score == 10)
        {
            PipesMove.speed = 2.8f;
            SecondGame.spawnTime = 1.6f;
        }
        if (_score == 15)
        {
            PipesMove.speed = 3.2f;
            SecondGame.spawnTime = 1.4f;
        }
        if (_score == 20)
        {
            PipesMove.speed = 3.6f;
            SecondGame.spawnTime = 1.2f;
        }
        if (_score == 25)
        {
            PipesMove.speed = 4f;
            SecondGame.spawnTime = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Score")
        {
            _score++;
            int balance = PlayerPrefs.GetInt("balance");
            PlayerPrefs.SetInt("balance", balance + 10);
        }
    }
}