using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Numbers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentButtonNumberText;
    private Action<int> clickedNumber;
    private int _currentButtonNumber;

    public void Initialize(int CurrentButtonNumber, Action<int> numberDelegate)
    {
        clickedNumber = numberDelegate;
        _currentButtonNumber = CurrentButtonNumber;
        CurrentButtonNumberText.text = CurrentButtonNumber.ToString();
    }
    public void OnClick()
    {
        clickedNumber(_currentButtonNumber);
        Destroy(gameObject);
    }
}
