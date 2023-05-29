using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondGame : MonoBehaviour
{
    [SerializeField] public GameObject pipes;
    public static float spawnTime = 2f;

    private void Start()
    {
        StartCoroutine(PipeSpawn());
    }

    private IEnumerator PipeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            float rand = Random.Range(-2f, 2f);
            GameObject newPipes = Instantiate(pipes, new Vector3(2, rand, 0), Quaternion.identity);
            Destroy(newPipes, 10f);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}