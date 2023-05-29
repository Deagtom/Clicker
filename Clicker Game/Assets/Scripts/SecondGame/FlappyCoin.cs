using UnityEngine;

public class FlappyCoin : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] public Rigidbody2D coingRigidBody;

    [SerializeField] public GameObject restartButton;
    [SerializeField] public GameObject menuButton;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void ClickOn()
    {
        coingRigidBody.velocity = Vector2.up * _force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Pipe")
        {
            Destroy(gameObject);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (GameObject gameObject in gameObjects)
            {
                Destroy(gameObject);
            }
            Time.timeScale = 0f;
            restartButton.SetActive(true);
            menuButton.SetActive(true);

            PipesMove.speed = 2f;
            SecondGame.spawnTime = 2f;
        }
    }
}