using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScoreManager : MonoBehaviour
{
    [SerializeField, Tooltip("The Multiplier that the player recieves when the rest bonus is active")]
    int RestBonusMultiplier;
    [SerializeField, Tooltip("Ammount of time the player has their rest bonus active when working")]
    float RestBonusLength;
    [SerializeField, Tooltip("Ammount of time the player must rest before the rest bonus can be earned")]
    float RequiredRestTime;
    [SerializeField, Tooltip("Ammount of time the player must work before the rest bonus can be earned")]
    float RequiredWorkTime;
    [SerializeField, Tooltip("Ammount of time the player to not trigger fatigue punishment with 10 taps")]
    float RequiredFatigueTriggerTime;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text fatigueText;

    bool _canRest = false;              // represents if the player has worked long enough to earn rest bonus
    bool _restBonusEarned = false;      // represents if the player has rested long eouugh to earn rest bonus
    bool _resting = false;              // represents whether the player is currently resting
    float _timeDifference = 0;
    int _score = 0;

    int _fatigueVal = 50;
    int tapTracker = 0;
    float _timeSinceFirstTap = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_resting)
        {
            if(_canRest && !_restBonusEarned)
            { 
                _timeDifference += Time.deltaTime;
                if(_timeDifference >= RequiredRestTime)
                {
                    _restBonusEarned = true;
                    _timeDifference = 0;
                    Debug.Log("REST BONUS EARNED");
                }
            }
        }
        else {
            if(!_canRest)
            {
                _timeDifference += Time.deltaTime;
                if(_timeDifference >= RequiredWorkTime)
                {
                    _canRest = true;
                    _timeDifference = 0;
                    Debug.Log("CAN START REST");
                }
            }
            else if (_restBonusEarned)
            {
                _timeDifference += Time.deltaTime;
                if( _timeDifference >= RestBonusLength)
                {
                    _restBonusEarned = false;
                    _canRest = false;
                    _timeDifference = 0;
                    Debug.Log("REST BONUS SPENT");
                }
            }
        }

        // Fatigue Level Tracking

        // reset tapTracker every 10 taps
        if(tapTracker == 10){
            Debug.Log("Time Since First Tap: " + _timeSinceFirstTap);
            if(_timeSinceFirstTap <= RequiredFatigueTriggerTime){
                _fatigueVal--;
            }

            tapTracker = 0;
            _timeSinceFirstTap = 0;
        }
        else{
            _timeSinceFirstTap += Time.deltaTime;
        }
        
    }

    public void ToggleResting()
    {
        _resting = !_resting;
    }

    public void OnButtonPress()
    {
        int score = 1;
        if(_restBonusEarned)
        {
            score *= RestBonusMultiplier;
        }
        _score += score;
        scoreText.text = _score.ToString();

        tapTracker++;
    }
}
