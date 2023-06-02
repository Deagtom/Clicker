using UnityEngine;

public class PipesMove : MonoBehaviour
{
    public static float speed = 2f;

    private void Update() => transform.Translate(-speed * Time.deltaTime, 0, 0);
}