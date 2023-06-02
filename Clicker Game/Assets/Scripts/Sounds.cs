using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip coinSound;
    public AudioClip touchSound;
    public AudioClip openSound;

    public void CoinSound() => audioSource.PlayOneShot(coinSound);

    public void ClickSound() => audioSource.PlayOneShot(clickSound);

    public void TouchSound() => audioSource.PlayOneShot(touchSound);

    public void OpenSound() => audioSource.PlayOneShot(openSound);
}