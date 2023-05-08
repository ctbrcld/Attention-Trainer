using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeBarText;
    [SerializeField] private TextMeshProUGUI _setTimerText;
    [SerializeField] private TextMeshProUGUI _setAmountText;
    [SerializeField] private TextMeshProUGUI _startButtonText;
    [SerializeField] private TextMeshProUGUI _winResultText;
    [SerializeField] private Slider _timeBarSlider;
    [SerializeField] private Slider _setTimerSlider;
    [SerializeField] private Slider _setAmountSlider;
    [SerializeField] private Numbers _numberButtonPrefab;
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _winSplashScreen;
    [SerializeField] private GameObject _loseSplashScreen;
    [SerializeField] private RectTransform _gameField;

    private int _expectedNumber;
    private int _amountOfNumbers;
    private float _timer;
    private float _setTimer;
    private float _maxX;
    private float _maxY;
    private bool _stopTimer;
    private const int MinSizeBorder = 40;

    private void Start()
    {
        ChangeParameters();
    }

    public void ChangeParameters()
    {
        _stopTimer = true;
        _setTimerSlider.interactable = true;
        _setAmountSlider.interactable = true;
        _startButton.interactable = true;
        _timeBarSlider.interactable = false;
        _timeBarText.text = "Set parameters below";
        _amountOfNumbers = (int)_setAmountSlider.value;
        _setAmountText.text = "Amount of numbers: " + _amountOfNumbers;
        _timer = _setTimerSlider.value;
        _setTimerText.text = "Game time: " + _setTimerSlider.value.ToString() + " sec";
    }

    public void StartGame()
    {
        _stopTimer = false;
        _setTimer = _setTimerSlider.value;
        _winSplashScreen.SetActive(false);
        _loseSplashScreen.SetActive(false);
        _setAmountSlider.interactable = false;
        _setTimerSlider.interactable = false;
        _timeBarText.text = "Time left: " + _setTimerSlider.value;
        _startButton.interactable = false;
        _startButtonText.text = "Hurry up!";
        _expectedNumber = 1;

        for (int i = 1; i <= _amountOfNumbers; i++)
        {
            var rect = _gameField.rect;
            _maxX = rect.size.x;
            _maxY = rect.size.y;
            float randomX = Random.Range(MinSizeBorder, _maxX - MinSizeBorder);
            float randomY = Random.Range(MinSizeBorder, _maxY - MinSizeBorder);
            var spawnedButton = Instantiate(_numberButtonPrefab, _gameField);
            spawnedButton.transform.localPosition = new Vector2(randomX, randomY);
            spawnedButton.Initialize(i, ClickedNumber);
        }
    }

    private void StopGame()
    {
        ChangeParameters();
        DestroyAllNumbers();
        _startButtonText.text = "Restart";
    }

    private void ClickedNumber(int clickedNumber)
    {
        if (clickedNumber != _expectedNumber)
        {
            Lose();
        }

        ++_expectedNumber;
        if (_expectedNumber == _amountOfNumbers + 1)
        {
            Win();
        }
    }

    private void Win()
    {
        _winSplashScreen.SetActive(true);
        _winResultText.text = (_setTimer - _timer).ToString("you did it in " + ("0.00") + " seconds");
        StopGame();
    }

    private void Lose()
    {
        _loseSplashScreen.SetActive(true);
        StopGame();
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
            _timer -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(_timer);
            float fraction = Mathf.FloorToInt((_timer - seconds) * 100);
            string timeLeftText = string.Format("{0:00}:{1:00}", seconds, fraction);
            _timeBarText.text = timeLeftText;
            _timeBarSlider.value = _timer / _setTimer;
        }

        if (_timer < 0)
        {
            Lose();
        }
    }
}
