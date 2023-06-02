using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private int _score;
    public Text scoreText;

    private void Update() => scoreText.text = _score.ToString();

    private void Start()
    {
        _score = 3;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        _score--;
        yield return new WaitForSeconds(1);
        _score--;
        yield return new WaitForSeconds(1);
        _score--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Score")
        {
            _score++;
            int balance = PlayerPrefs.GetInt("balance");
            PlayerPrefs.SetInt("balance", balance + 10);
            if (SecondGame.spawnTime >= 1.535f && PipesMove.speed <= 4.95f)
            {
                SecondGame.spawnTime -= 0.035f;
                PipesMove.speed += 0.05f;
            }
        }
    }
}