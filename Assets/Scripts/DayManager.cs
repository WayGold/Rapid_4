using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{

    [SerializeField, Tooltip("The ammount of Real World (RW) Seconds that equates to Game World (GW) 15 minutes")]
    float RWSeconds2GW15Min = 5;
    [SerializeField, Tooltip("The 24 hours time of day work starts")]
    int WorkStartTime = 8;
    [SerializeField, Tooltip("The 24 hours time of day work ends")]
    int WorkEndTime = 16;
    [SerializeField]
    UnityEvent<float> OnTimeUpdate = new UnityEvent<float>();
    [SerializeField]
    ButtonScoreManager scoreManager;


    float _gameTime;
    float _timePassed;
    float _workEndOfDay;

    float _lastScore;
    float _scoreDiff = 0;
    int _numScoreChecks = 0;
    float _lastFatigue;
    float _fatigueDiff = 0;
    int _numFatigueChecks = 0;

    bool _initialUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
        _lastScore = scoreManager.GetScore();
        _lastFatigue = scoreManager.GetFatigue();
        _gameTime = WorkStartTime;
        _timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_initialUpdate)
        {
            OnTimeUpdate.Invoke(_gameTime);
            _initialUpdate = false;
        }
        _timePassed += Time.deltaTime;
        while (_timePassed >= RWSeconds2GW15Min)
        {
            _timePassed -= RWSeconds2GW15Min;
            _gameTime += 0.25f;

            var newScore = scoreManager.GetScore();
            var newFatigue = scoreManager.GetFatigue();
            _scoreDiff += (newScore - _lastScore);
            _fatigueDiff += (_lastFatigue - newFatigue);
            _lastScore = newScore;
            _lastFatigue = newFatigue;
            _numScoreChecks++;
            _numFatigueChecks++;
            OnTimeUpdate.Invoke(_gameTime);
            if(_gameTime >= WorkEndTime)
            {
                PlayerPrefs.SetFloat("FatigueDiff", _fatigueDiff / _numFatigueChecks);
                PlayerPrefs.SetFloat("ScoreDiff", _scoreDiff / _numScoreChecks);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Statistics");
            }
        }
    }
}
