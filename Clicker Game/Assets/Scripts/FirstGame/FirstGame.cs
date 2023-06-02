using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FirstGame : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private GameObject _scrollPanel;
    [SerializeField] private Text _balanceText;
    [SerializeField] private int _balance;

    private bool _isOpen = false;
    private int _bet;
    private float _scrollSpeed;
    private float _velocity;

    [SerializeField] private bool _isRed = false;
    [SerializeField] private bool _isBlack = false;
    [SerializeField] private bool _isGreen = false;

    [SerializeField] private int _attemptsForMiniGames;
    [SerializeField] private Text _attemptsText;

    public AudioSource audioSource;

    public void RedClick()
    {
        if (!_isOpen)
            _isRed = true;
    }

    public void BlackClick()
    {
        if (!_isOpen)
            _isBlack = true;
    }

    public void GreenClick()
    {
        if (!_isOpen)
            _isGreen = true;
    }

    private void Start()
    {
        _attemptsForMiniGames = PlayerPrefs.HasKey("attempts for mini games") ? PlayerPrefs.GetInt("attempts for mini games") : 0;
        _balance = PlayerPrefs.GetInt("balance");
        CreateRedAndBlack();
    }

    public void StartGame()
    {
        if (int.Parse(_inputField.text) <= _balance && !_isOpen && _attemptsForMiniGames > 0)
        {
            _bet = int.Parse(_inputField.text);
            _balance -= _bet;
            PlayerPrefs.SetInt("balance", _balance);
            _velocity = Random.Range(2.3f, 2.8f);
            _isOpen = true;
            _scrollPanel.transform.localPosition = new Vector2(Random.Range(25000, 20000), 0);
            _scrollSpeed = -25f;

            _attemptsForMiniGames--;
            PlayerPrefs.SetInt("attempts for mini games", _attemptsForMiniGames);
        }
    }

    private void ShowBalance()
    {
        if (_balance >= 1000000)
            _balanceText.text = (_balance / 1000).ToString() + " K";
        else
            _balanceText.text = _balance.ToString();
    }

    private void CreateRedAndBlack()
    {
        for (int i = 0; i < 150; i++)
        {
            GameObject red = Instantiate(_prefabs[0], new Vector2(0, 0), Quaternion.identity) as GameObject;
            red.transform.SetParent(_scrollPanel.transform);
            red.transform.localScale = new Vector2(1, 1);
            GameObject black = Instantiate(_prefabs[1], new Vector2(0, 0), Quaternion.identity) as GameObject;
            black.transform.SetParent(_scrollPanel.transform);
            black.transform.localScale = new Vector2(1, 1);
            if (i % 7 == 0)
            {
                GameObject green = Instantiate(_prefabs[2], new Vector2(0, 0), Quaternion.identity) as GameObject;
                green.transform.SetParent(_scrollPanel.transform);
                green.transform.localScale = new Vector2(1, 1);
            }
        }
    }

    private void Update()
    {
        _attemptsText.text = "Attempts: " + _attemptsForMiniGames;
        ShowBalance();
        if (_isOpen)
        {
            _scrollPanel.transform.Translate(new Vector2(_scrollSpeed, 0) * Time.deltaTime);
            _scrollSpeed = Mathf.MoveTowards(_scrollSpeed, 0, _velocity * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            if (hit.collider != null && _scrollSpeed == 0f)
            {
                if (hit.collider.tag == "Red" && _isRed)
                {
                    _balance += _bet * 2;
                    PlayerPrefs.SetInt("balance", _balance);
                }
                else if (hit.collider.tag == "Black" && _isBlack)
                {
                    _balance += _bet * 2;
                    PlayerPrefs.SetInt("balance", _balance);
                }
                else if (hit.collider.tag == "Green" && _isGreen)
                {
                    _balance += _bet * 14;
                    PlayerPrefs.SetInt("balance", _balance);
                }
                _isOpen = false;
                _isBlack = false;
                _isRed = false;
                _isGreen = false;
                _balance = PlayerPrefs.GetInt("balance");
            }
            else if (_scrollSpeed == 0f)
                _scrollPanel.transform.Translate(new Vector2(-5f, 0) * Time.deltaTime);
        }
    }
}