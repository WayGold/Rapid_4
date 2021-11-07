using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticDisplay : MonoBehaviour
{
    [SerializeField]
    Text AvgFatigueText;
    [SerializeField]
    Text AvgScoreText;
    [SerializeField]
    Text OverallResultText;
    [SerializeField]
    int breakInterval;
    [SerializeField]
    float ExpectedOutput;


    float _avgFatigueChange;
    float _avgScoreChange;

    // Start is called before the first frame update
    void Start()
    {
        _avgFatigueChange = PlayerPrefs.GetFloat("FatigueDiff");
        _avgScoreChange = PlayerPrefs.GetFloat("ScoreDiff");
        AvgFatigueText.text = _avgFatigueChange.ToString();
        AvgScoreText.text = _avgScoreChange.ToString();
        string SuggestionText = "";
        if(_avgFatigueChange > 0)
        {
            SuggestionText = "Try reducing your burnout by taking a break every: ";
            SuggestionText += breakInterval.ToString() + " minutes\n\n";
        }
        if(_avgScoreChange <= ExpectedOutput)
        {
            SuggestionText += "Don't Over or Underwork yourself or you will experience more burnout\n\n";
            SuggestionText += "Keeping at a steady/consistent pace helps reduce burnout\n";
        }
        OverallResultText.text = SuggestionText;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
