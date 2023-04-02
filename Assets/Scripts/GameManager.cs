using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _amountSlider;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private RectTransform _gameField;
    [SerializeField] private Numbers _buttonPrefab;
    [SerializeField] private GameObject _winSplashScreen;
    [SerializeField] private GameObject _loseSplashScreen;

    private GameState _gameState = GameState.Idle;
    private int _expectedButton = 1;
    private int _amount;
    private float _maxX;
    private float _maxY;
    private const int MinSizeBorder = 40;
    private List<Numbers> _numbers = new();

    public void AmountChange()
    {
        _amount = (int)_amountSlider.value;
        _amountText.text = "Amount of Numbers: " + _amount;
    }

    private void Win()
    {
        Debug.Log("You Win!");
    }

    private void Lose()
    {
        foreach (Numbers num in _numbers)
        {
            Destroy(num.gameObject);
        }
        _numbers.Clear();
        _loseSplashScreen.SetActive(true);
    }

    private void ButtonClicked(int number)
    {
        _numbers.Remove(_numbers[0]);
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

    public void StartGame()
    {
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
            _numbers.Add(spawnedButton);
            counter++;
        }
    }
}
