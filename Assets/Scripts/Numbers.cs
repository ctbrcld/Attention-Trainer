using System;
using TMPro;
using UnityEngine;

public class Numbers : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentNumberButtonText;
    private Action<int> _clickedNumberButton;
    private int _currentButtonNumber;

    public void Initialize(int CurrentNumberButton, Action<int> numberButtonDelegate)
    {
        _clickedNumberButton = numberButtonDelegate;
        _currentButtonNumber = CurrentNumberButton;
        _currentNumberButtonText.text = CurrentNumberButton.ToString();
    }
    public void OnClick()
    {
        _clickedNumberButton(_currentButtonNumber);
        Destroy(gameObject);
    }
}
