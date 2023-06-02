using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sliderVolume;
    public static bool mute;

    public Image volume;
    public Sprite offSoundSprite;
    public Sprite onSoundSprite;

    public Button button;
    public Sprite offMusicSprite;
    public Sprite onMusicSprite;

    public void ToMenu() => SceneManager.LoadScene(0);

    private void Awake()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volume");
        ChangeSprite();
    }

    public void OnOffMusic()
    {
        mute = !mute;
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (mute)
            button.image.sprite = offMusicSprite;
        else
            button.image.sprite = onMusicSprite;
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("balance", 0);
        PlayerPrefs.SetInt("factor", 1);
    }

    private void Update()
    {
        if (sliderVolume.value <= 0f)
            volume.sprite = offSoundSprite;
        else
            volume.sprite = onSoundSprite;
        PlayerPrefs.SetFloat("volume", sliderVolume.value);
        PlayerPrefs.Save();
        AudioListener.volume = sliderVolume.value;
    }
}