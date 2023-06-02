using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music _instance;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    private void Update()
    {
        if (SettingsMenu.mute)
            gameObject.GetComponent<AudioSource>().mute = true;
        else
            gameObject.GetComponent<AudioSource>().mute = false;
    }
}