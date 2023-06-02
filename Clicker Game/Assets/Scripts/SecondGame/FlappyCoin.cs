using UnityEngine;

public class FlappyCoin : MonoBehaviour
{
    [SerializeField] private float _force;
    public Rigidbody2D coingRigidBody;

    public GameObject restartButton;

    public AudioSource audioSource;
    public AudioClip dieSound;

    public void ClickOn() => coingRigidBody.velocity = Vector2.up * _force;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Pipe")
        {
            audioSource.PlayOneShot(dieSound);
            Destroy(gameObject);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (GameObject gameObject in gameObjects)
                Destroy(gameObject);
            Time.timeScale = 0f;
            restartButton.SetActive(true);
            PipesMove.speed = 2f;
            SecondGame.spawnTime = 2f;
        }
    }
}