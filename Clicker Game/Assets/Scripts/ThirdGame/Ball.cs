using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject right;
    public GameObject left;

    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private int _score;
    public Text scoreText;

    public AudioSource audioSource;
    public AudioClip dieSound;
    public AudioClip touchSound;

    [SerializeField] private Sprite spriteOne;
    [SerializeField] private Sprite spriteTwo;

    public GameObject stick;

    private void Start() => ball.velocity = new Vector2(Random.Range(-0.5f, 0.5f), 4f);

    private void Update()
    {
        scoreText.text = _score.ToString();
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("EnemySecond").Length == 0 && GameObject.FindGameObjectsWithTag("EnemyThird").Length == 0)
        {
            stick.SetActive(false);
            _score += 300;
            scoreText.text = _score.ToString();
            int balance = PlayerPrefs.GetInt("balance");
            PlayerPrefs.SetInt("balance", balance + 300);
            Destroy(gameObject);
            Time.timeScale = 0f;
            restartButton.SetActive(true);
            right.SetActive(false);
            left.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Pipe")
        {
            stick.SetActive(false);
            audioSource.PlayOneShot(dieSound);
            Destroy(gameObject);
            Time.timeScale = 0f;
            restartButton.SetActive(true);
            right.SetActive(false);
            left.SetActive(false);
        }

        if (collision.collider.tag == "Stick")
        {
            float collisionPointX = collision.contacts[0].point.x;
            ball.velocity = Vector2.zero;
            float stickCenterPosition = stick.GetComponent<Transform>().position.x;
            float difference = stickCenterPosition - collisionPointX;
            float direction = collisionPointX < stickCenterPosition ? -1 : 1;
            ball.AddForce(new Vector2(direction * Mathf.Abs(difference * 0.01f), 0.02f));
        }

        if (collision.collider.tag == "Enemy")
        {
            audioSource.PlayOneShot(touchSound);
            _score += 3;
            Destroy(collision.gameObject);
            int balance = PlayerPrefs.GetInt("balance");
            PlayerPrefs.SetInt("balance", balance + 3);
        }
        else if (collision.collider.tag == "EnemySecond")
        {
            audioSource.PlayOneShot(touchSound);
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = spriteTwo;
            collision.gameObject.tag = "Enemy";
        }
        else if (collision.collider.tag == "EnemyThird")
        {
            audioSource.PlayOneShot(touchSound);
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOne;
            collision.gameObject.tag = "EnemySecond";
        }
    }
}