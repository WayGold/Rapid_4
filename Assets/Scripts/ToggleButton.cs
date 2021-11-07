using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    string OnText;
    [SerializeField]
    string OffText;
    [SerializeField]
    Animator animator;
    [SerializeField]
    UnityEvent<bool> ToggleEvent;

    bool _isOn = false;
    Text _buttonText;

    private void Start()
    {
        _buttonText = GetComponentInChildren<Text>();
        SetButtonText();
        if (_isOn && animator != null)
        {
            animator.SetBool("isResting", true);
        }
        if (ToggleEvent == null)
        {
            ToggleEvent = new UnityEvent<bool>();
        }
    }

    void SetButtonText()
    {
        _buttonText.text = _isOn ? OnText : OffText;
    }

    public void OnToggle()
    {
        _isOn = !_isOn;
        SetButtonText();
        ToggleEvent.Invoke(_isOn);
    }
}
