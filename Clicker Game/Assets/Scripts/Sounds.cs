using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip coinSound;
    public void CoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}