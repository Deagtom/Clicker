using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] int _balance;
    private int _factor;
    private int _count;
    private int _price;
    private int[] _priceFactor = { 10, 25, 50, 100, 250, 500, 1000, 5000, 9999 };
    private bool _isActivated;
    private const int _ActivatedIdleFarmPrice = 9999;

    private List<GameObject> _listGameObjects = new List<GameObject>();
    private VerticalLayoutGroup _group;

    public string[] arrayTitles;
    public Sprite[] arraySprites;
    public GameObject button;
    public GameObject content;
    public AudioSource audioSource;
    public AudioClip clickSound;

    private void Start()
    {
        _balance = PlayerPrefs.GetInt("balance");
        _factor = PlayerPrefs.GetInt("factor") != 0 ? PlayerPrefs.GetInt("factor") : 1;
        _count = PlayerPrefs.GetInt("count");
        _price = PlayerPrefs.GetInt("price") != 0 ? PlayerPrefs.GetInt("price") : _priceFactor[_count];
        _isActivated = PlayerPrefs.GetInt("is activated") == 1 ? true : false;

        RectTransform rectTransform = content.GetComponent<RectTransform>();
        rectTransform.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        _group = GetComponent<VerticalLayoutGroup>();
        SetUpgrades();

        _listGameObjects[1].GetComponentsInChildren<Text>()[1].text = _ActivatedIdleFarmPrice.ToString() + "$";
        CheckSoldUpgrade();
    }

    private void RemovedList()
    {
        foreach (var item in _listGameObjects)
        {
            Destroy(item);
        }
        _listGameObjects.Clear();
    }

    private void SetUpgrades()
    {
        RectTransform rectTransform = content.GetComponent<RectTransform>();
        rectTransform.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        RemovedList();
        if (arrayTitles.Length > 0)
        {
            var upgradeSampleFirst = Instantiate(button, transform);
            var height = upgradeSampleFirst.GetComponent<RectTransform>().rect.height;
            var transformPropeties = GetComponent<RectTransform>();
            transformPropeties.sizeDelta = new Vector2(transformPropeties.rect.width, height * arrayTitles.Length);
            Destroy(upgradeSampleFirst);
            for (int i = 0; i < arrayTitles.Length; i++)
            {
                var upgradeSampleSecond = Instantiate(button, transform);
                upgradeSampleSecond.GetComponentsInChildren<Text>()[0].text = arrayTitles[i];
                upgradeSampleSecond.GetComponentsInChildren<Text>()[1].text = _price.ToString() + "$";
                upgradeSampleSecond.GetComponentsInChildren<Image>()[1].sprite = arraySprites[i];
                var j = i;
                upgradeSampleSecond.GetComponent<Button>().onClick.AddListener(() => GetUpgrade(j));
                _listGameObjects.Add(upgradeSampleSecond);
            }
        }
    }

    private void GetUpgrade(int id)
    {
        switch (id)
        {
            case 0:
                UpgradeClicker();
                break;

            case 1:
                ActivatedAutoClick();
                break;
        }
    }

    private void UpgradeClicker()
    {
        audioSource.PlayOneShot(clickSound);
        if (_count < _priceFactor.Length && _balance >= _price)
        {
            _balance -= _price;
            _count += 1;
            PlayerPrefs.SetInt("balance", _balance);
            PlayerPrefs.SetInt("factor", _factor + 1);
            if (_count < _priceFactor.Length)
            {
                PlayerPrefs.SetInt("price", _priceFactor[_count]);
            }
            else
            {
                CheckSoldUpgrade();
            }
            PlayerPrefs.SetInt("count", _count);
            _price = PlayerPrefs.GetInt("price");
            _factor = PlayerPrefs.GetInt("factor");
            _balance = PlayerPrefs.GetInt("balance");
            _listGameObjects[0].GetComponentsInChildren<Text>()[1].text = _price.ToString() + "$";
            CheckSoldUpgrade();
        }
    }

    private void ActivatedAutoClick()
    {
        audioSource.PlayOneShot(clickSound);
        if (!_isActivated && _balance >= _ActivatedIdleFarmPrice)
        {
            _balance -= _ActivatedIdleFarmPrice;
            _isActivated = true;
            PlayerPrefs.SetInt("balance", _balance);
            PlayerPrefs.SetInt("is activated", 1);
            StartCoroutine(IdleFarm());
            CheckSoldUpgrade();
        }
    }

    private void CheckSoldUpgrade()
    {
        if (_count >= _priceFactor.Length)
        {
            _listGameObjects[0].GetComponentsInChildren<Text>()[1].text = "Sold";
        }
        if (_isActivated)
        {
            _listGameObjects[1].GetComponentsInChildren<Text>()[1].text = "Sold";
        }
    }

    private IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        _balance++;
        PlayerPrefs.SetInt("balance", _balance);
        StartCoroutine(IdleFarm());
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("volume");
    }
}