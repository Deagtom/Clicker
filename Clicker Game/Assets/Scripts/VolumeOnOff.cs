using UnityEngine;
using UnityEngine.UI;

public class VolumeOnOff : MonoBehaviour
{
    private bool _isOn;

    public Button button;
    public Sprite OffSoundSprite;
    public Sprite OnSoundSprite;

    private void Start()
    {
        _isOn = PlayerPrefs.GetInt("volume") != 0 ? PlayerPrefs.GetInt("volume") == 1 ? true : false : true;
        button.image.sprite = _isOn ? OnSoundSprite : OffSoundSprite;
    }

    public void OnOffSounds()
    {
        if (!_isOn)
        {
            AudioListener.volume = 1f;
            _isOn = true;
            PlayerPrefs.SetInt("volume", 1);
            button.image.sprite = OnSoundSprite;
        }
        else
        {
            AudioListener.volume = 0f;
            _isOn = false;
            PlayerPrefs.SetInt("volume", 2);
            button.image.sprite = OffSoundSprite;
        }
    }
}
