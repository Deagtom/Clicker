using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _balance;
    [SerializeField] private int _factor;
    private bool _isActivated;

    public Text balanceText;
    public GameObject effect;
    public GameObject button;

    [SerializeField] private int _countClick = 0;
    [SerializeField] private float _delay = 5;

    private Animator _animator;

    private void Start()
    {
        Time.timeScale = 1f;
        _balance = PlayerPrefs.GetInt("balance");
        _factor = PlayerPrefs.GetInt("factor") != 0 ? PlayerPrefs.GetInt("factor") : 1;
        PlayerPrefs.SetInt("factor", _factor);

        _isActivated = PlayerPrefs.GetInt("is activated") == 1 ? true : false;
        if (_isActivated)
        {
            StartCoroutine(IdleFarm());
        }

        GetOfflineBalance();

        if (!PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
    }

    public void ButtonClick()
    {
        _balance += _factor;
        PlayerPrefs.SetInt("balance", _balance);
        Instantiate(effect, button.GetComponent<RectTransform>().position.normalized, Quaternion.identity);
        button.GetComponent<RectTransform>().localScale = new Vector3(0.95f, 0.95f, 0f);
        _countClick++;
    }

    public void OnClickUp()
    {
        button.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0f);
    }

    private IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        _balance++;
        PlayerPrefs.SetInt("balance", _balance);
        StartCoroutine(IdleFarm());
    }

    private void ShowBalance()
    {
        if (_balance >= 1000000)
        {
            balanceText.text = (_balance / 1000).ToString() + " K";
        }
        else
        {
            balanceText.text = _balance.ToString();
        }
    }

    private void GetOfflineBalance()
    {
        TimeSpan totalSecond;
        if (PlayerPrefs.GetInt("is activated") == 1)
        {
            totalSecond = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("last session"));
            _balance += (int)totalSecond.TotalSeconds;
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetString("last session", DateTime.Now.ToString());
        }
    }
#else
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("last session", DateTime.Now.ToString());
    }
#endif

    public void ToUpgrades()
    {
        PlayerPrefs.SetString("last session", DateTime.Now.ToString());
        SceneManager.LoadScene(1);
    }

    public void ToSettings()
    {
        PlayerPrefs.SetString("last session", DateTime.Now.ToString());
        SceneManager.LoadScene(2);
    }

    public void ToLevels()
    {
        PlayerPrefs.SetString("last session", DateTime.Now.ToString());
        SceneManager.LoadScene(3);
    }
    private IEnumerator AnimatorStop()
    {
        yield return new WaitForSeconds(1.5f);
        _animator.enabled = false;
    }

    private void IsAutoclickerUsed()
    {
        if (_delay > 0)
        {
            _delay -= Time.fixedDeltaTime;

            if (_delay <= 0)
            {
                if (_countClick > 150f)
                {
                    _animator = GetComponent<Animator>();
                    _animator.enabled = true;
                    StartCoroutine(AnimatorStop());
                    _balance = 0;
                }
                _countClick = 0;
                _delay = 5f;
            }
        }
    }

    private void FixedUpdate()
    {
        ShowBalance();
        IsAutoclickerUsed();
    }
}