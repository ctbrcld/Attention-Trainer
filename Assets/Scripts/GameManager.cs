using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _barTimeText;
    [SerializeField] private TextMeshProUGUI _setTimeText;
    [SerializeField] private TextMeshProUGUI _setAmountText;
    [SerializeField] private TextMeshProUGUI _startButtonText;
    [SerializeField] private Slider _barTimeSlider;
    [SerializeField] private Slider _setTimeSlider;
    [SerializeField] private Slider _setAmountSlider;
    [SerializeField] private Numbers _buttonPrefab;
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _winSplashScreen;
    [SerializeField] private GameObject _loseSplashScreen;
    [SerializeField] private RectTransform _gameField;

    private int _expectedButton = 1;
    private int _amount;
    private float _time;
    private float _setTime;
    private float _maxX;
    private float _maxY;
    private bool _stopTimer;
    private const int MinSizeBorder = 40;

    private void Start()
    {
        _stopTimer = true;
        _barTimeSlider.interactable = false;
        _time = _setTimeSlider.value;

        _amount = (int)_setAmountSlider.value;
        _setAmountText.text = "Amount of numbers: " + _amount;
        _setTimeText.text = "Game time: " + _setTimeSlider.value.ToString() + " sec";
    }

    public void AmountChange()
    {
        _amount = (int)_setAmountSlider.value;
        _setAmountText.text = "Amount of numbers: " + _amount;
    }

    public void GameTimeChange()
    {
        _time = _setTimeSlider.value;
        _setTimeText.text = "Game time: " + _setTimeSlider.value.ToString() + " sec";
        _barTimeText.text = "Set parameters below";
    }

    public void StartGame()
    {
        _setTime = _setTimeSlider.value;
        _stopTimer = false;
        _winSplashScreen.SetActive(false);
        _loseSplashScreen.SetActive(false);
        _startButton.interactable = false;
        _setTimeSlider.interactable = false;
        _setAmountSlider.interactable = false;

        _barTimeText.text = "Time left: " + _setTimeSlider.value;
        _startButtonText.text = "Hurry up!";

        _expectedButton = 1;

        var counter = 1;
        while (counter <= _amount)
        {
            var rect = _gameField.rect;
            _maxX = rect.size.x;
            _maxY = rect.size.y;
            float randomX = Random.Range(MinSizeBorder, _maxX - MinSizeBorder);
            float randomY = Random.Range(MinSizeBorder, _maxY - MinSizeBorder);
            var spawnedButton = Instantiate(_buttonPrefab, _gameField);
            spawnedButton.transform.localPosition = new Vector2(randomX, randomY);
            spawnedButton.Initialize(counter, ButtonClicked);
            counter++;
        }
    }

    private void ButtonClicked(int number)
    {
        if (number != _expectedButton)
        {
            Lose();
        }
        ++_expectedButton;
        if (_expectedButton == _amount + 1)
        {
            Win();
        }
    }

    private void Win()
    {
        AmountChange();
        GameTimeChange();
        DestroyAllNumbers();
        _stopTimer = true;
        _winSplashScreen.SetActive(true);
        _barTimeText.text = "Set parameters below";
        _setTimeSlider.interactable = true;
        _setAmountSlider.interactable = true;
        _startButtonText.text = "Restart";
        _startButton.interactable = true;
    }

    private void Lose()
    {
        AmountChange();
        GameTimeChange();
        DestroyAllNumbers();
        _stopTimer = true;
        _loseSplashScreen.SetActive(true);
        _barTimeText.text = "Set parameters below";
        _setTimeSlider.interactable = true;
        _setAmountSlider.interactable = true;
        _startButtonText.text = "Restart";
        _startButton.interactable = true;
    }

    public void DestroyAllNumbers()
    {
        GameObject[] numbers = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < numbers.Length; i++)
        {
            Destroy(numbers[i]);
        }
    }

    private void Update()
    {

        if (!_stopTimer)
        {
            _time -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(_time / 60);
            float seconds = Mathf.FloorToInt(_time - minutes * 60);
            string timeLeftText = string.Format("{0:0}:{1:00}", minutes, seconds);
            _barTimeText.text = timeLeftText;
            _barTimeSlider.value = _time / _setTime;

        }

        if (_time < 0)
        {
            Lose();
        }
    }

}
