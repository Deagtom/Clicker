using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] int _balance;
    private int _factor;
    private int _count;
    private int _price;
    private readonly int[] _priceFactor = { 10, 25, 50, 100, 250, 500, 1000, 5000, 9999 };
    private bool _isActivated;
    private const int _ActivatedIdleFarmPrice = 9999;
    private const int _OneAttempt = 99;

    public GameObject[] button;
    [SerializeField] private Text _balanceText;

    [SerializeField] private int _attemptsForMiniGames;
    private int _countAttempts;

    public void ToMainMenu() => SceneManager.LoadScene(0);

    private void ShowBalance()
    {
        if (_balance >= 1000000)
            _balanceText.text = (_balance / 1000).ToString() + " K";
        else
            _balanceText.text = _balance.ToString();
    }

    private void Update()
    {
        ShowBalance();
        button[2].GetComponentsInChildren<Text>()[1].text = (_OneAttempt * _countAttempts).ToString() + "$";

        if (int.TryParse(button[2].GetComponentsInChildren<InputField>()[0].text, out int value))
            _countAttempts = int.Parse(button[2].GetComponentsInChildren<InputField>()[0].text);
        else
            _countAttempts = 0;
    }

    private void Start()
    {
        _balance = PlayerPrefs.GetInt("balance");
        _factor = PlayerPrefs.GetInt("factor") != 0 ? PlayerPrefs.GetInt("factor") : 1;
        _count = PlayerPrefs.GetInt("count");
        _price = PlayerPrefs.GetInt("price") != 0 ? PlayerPrefs.GetInt("price") : _priceFactor[_count];
        _isActivated = PlayerPrefs.GetInt("is activated") == 1 ? true : false;

        CheckSoldUpgrade();
        _attemptsForMiniGames = PlayerPrefs.HasKey("attempts for mini games") ? PlayerPrefs.GetInt("attempts for mini games") : 0;
    }

    public void BuyOneAttemptForMiniGames()
    {
        if (_balance >= _OneAttempt * _countAttempts)
        {
            _balance -= _OneAttempt * _countAttempts;
            PlayerPrefs.SetInt("balance", _balance);
            PlayerPrefs.SetInt("attempts for mini games", _attemptsForMiniGames + _countAttempts);
            _attemptsForMiniGames = PlayerPrefs.GetInt("attempts for mini games");
            _balance = PlayerPrefs.GetInt("balance");
        }
    }

    public void UpgradeClicker()
    {
        if (_count < _priceFactor.Length && _balance >= _price)
        {
            _balance -= _price;
            _count += 1;
            PlayerPrefs.SetInt("balance", _balance);
            PlayerPrefs.SetInt("factor", _factor + 1);

            if (_count < _priceFactor.Length)
                PlayerPrefs.SetInt("price", _priceFactor[_count]);
            else
                CheckSoldUpgrade();

            PlayerPrefs.SetInt("count", _count);
            _price = PlayerPrefs.GetInt("price");
            _factor = PlayerPrefs.GetInt("factor");
            _balance = PlayerPrefs.GetInt("balance");
            button[0].GetComponentsInChildren<Text>()[1].text = _price.ToString() + "$";
            CheckSoldUpgrade();
        }
    }

    public void ActivatedAutoClick()
    {
        if (!_isActivated && _balance >= _ActivatedIdleFarmPrice)
        {
            _balance -= _ActivatedIdleFarmPrice;
            _isActivated = true;
            PlayerPrefs.SetInt("balance", _balance);
            PlayerPrefs.SetInt("is activated", 1);
            _balance = PlayerPrefs.GetInt("balance");
            StartCoroutine(IdleFarm());
            CheckSoldUpgrade();
        }
    }

    private void CheckSoldUpgrade()
    {
        if (_count >= _priceFactor.Length)
            button[0].GetComponentsInChildren<Text>()[1].text = "Sold";
        if (_isActivated)
            button[1].GetComponentsInChildren<Text>()[1].text = "Sold";
    }

    private IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);
        _balance++;
        PlayerPrefs.SetInt("balance", _balance);
        StartCoroutine(IdleFarm());
    }
}