using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleTextSwitch : MonoBehaviour
{
    Toggle _toggle;
    Text _toggleText;

    [SerializeField]
    string NormalText;
    [SerializeField]
    string ToggleText;

    void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggleText = GetComponentInChildren<Text>();
        _toggleText.text = NormalText;
    }

    public void ToggleButton()
    {
        _toggleText.text = _toggle.isOn ? NormalText : ToggleText;
    }
}
