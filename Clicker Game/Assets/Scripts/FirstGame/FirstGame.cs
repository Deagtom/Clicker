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
    private bool _isRed;

    private void Start()
    {
        _balance = PlayerPrefs.GetInt("balance");
        CreateRedAndBlack();
    }

    public void StartGame()
    {
        if (_inputField.text != string.Empty && _inputField.text.All(char.IsDigit) && int.Parse(_inputField.text) <= _balance && !_isOpen)
        {
            _bet = int.Parse(_inputField.text);
            _balance -= _bet;
            PlayerPrefs.SetInt("balance", _balance);
            _velocity = Random.Range(6f, 10f);
            _isOpen = true;
            _scrollPanel.transform.localPosition = new Vector2(0, 0);
            _scrollSpeed = -22f;
        }
    }

    public void RedClick() => _isRed = true;

    public void BlackClick() => _isRed = false;

    private void CreateRedAndBlack()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject _red = Instantiate(_prefabs[0], new Vector2(0, 0), Quaternion.identity) as GameObject;
            _red.transform.SetParent(_scrollPanel.transform);
            _red.transform.localScale = new Vector2(1, 1);
            GameObject _black = Instantiate(_prefabs[1], new Vector2(0, 0), Quaternion.identity) as GameObject;
            _black.transform.SetParent(_scrollPanel.transform);
            _black.transform.localScale = new Vector2(1, 1);
        }
    }

    private void Update()
    {
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
                else if (hit.collider.tag == "Black" && !_isRed)
                {
                    _balance += _bet * 2;
                    PlayerPrefs.SetInt("balance", _balance);
                }
                _isOpen = false;
            }
            else if (_scrollSpeed == 0f)
            {
                _scrollPanel.transform.Translate(new Vector2(-5f, 0) * Time.deltaTime);
            }
        }
    }

    private void ShowBalance()
    {
        if (_balance >= 1000000)
        {
            _balanceText.text = (_balance / 1000).ToString() + " K";
        }
        else
        {
            _balanceText.text = _balance.ToString();
        }
    }
}