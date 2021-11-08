using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameWorldClock : MonoBehaviour
{
    Text textField;
    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<Text>();
        textField.text = Decimal2Time(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Decimal2Time(float decVal)
    {
        int WholeNumber = (int)decVal;

        while (WholeNumber >= 24)
        {
            WholeNumber -= 24;
        }
        string TimeString = WholeNumber.ToString();
        if(WholeNumber < 10)
        {
            TimeString = "0" + TimeString;
        }
        int NumMinutes = (int)((decVal - WholeNumber) * 60);
        if(NumMinutes < 10)
        {
            TimeString += "0";
        }
        TimeString += NumMinutes.ToString();
        return TimeString;
    }

    public void UpdateClockText(float newTime)
    {
        textField.text = Decimal2Time(newTime);
    }
}
