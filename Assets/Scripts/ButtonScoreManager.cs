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
    [SerializeField]
    Text scoreText;

    bool _canRest = false;              // represents if the player has worked long enough to earn rest bonus
    bool _restBonusEarned = false;      // represents if the player has rested long eouugh to earn rest bonus
    bool _resting = false;              // represents whether the player is currently resting
    float _timeDifference = 0;
    int _score = 0;

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
    }
}
