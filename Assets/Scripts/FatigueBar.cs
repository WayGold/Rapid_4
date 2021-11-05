using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatigueBar : MonoBehaviour
{
    public Text FatigueText;

    public Image FatigueBarImage;

    public float LerpSpeed = 3.0f;
    
    public float CurrentFatigue, MaxFatigue = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        LerpSpeed *= Time.deltaTime;


    }

    // Update is called once per frame
    void Update()
    {
        FatigueText.text = "Fatigue: " + CurrentFatigue + "%";
        if (CurrentFatigue >MaxFatigue)
        {
            CurrentFatigue = MaxFatigue;
        }
        
        FatigueBarFiler();
        
    }
    
    void FatigueBarFiler()
    {
        FatigueBarImage.fillAmount = Mathf.Lerp(FatigueBarImage.fillAmount,CurrentFatigue/MaxFatigue,LerpSpeed);
    }
    
    
    public void DecreaseFatigue(float Points)
    {
        if (CurrentFatigue>0)
        {
            CurrentFatigue = CurrentFatigue - Points;
        }
    }
    
    public void IncreaseFatigue(float Points)
    {
        if (CurrentFatigue < MaxFatigue)
        {
            CurrentFatigue = CurrentFatigue + Points;
        }
    }
}
