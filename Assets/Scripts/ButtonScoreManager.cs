using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScoreManager : MonoBehaviour
{
    [SerializeField]
    int RestBonusMultiplier;
    [SerializeField]
    float RestBonusLength;
    [SerializeField]
    float TimeTillRestBonus;
    [SerializeField]
    float TimeTillCanRest;
    [SerializeField]
    Text scoreText;

    bool _restBonusActive = false;
    bool _canStartRest = false;
    bool _canRest = false;
    float _timeDifference = 0;
    int _score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canStartRest)
        {
            _timeDifference += Time.deltaTime;
            if (_timeDifference >= TimeTillCanRest)
            {
                _canStartRest = true;
                _timeDifference = 0;
                Debug.Log("CAN START REST");
            }
        }
        else if (!_canRest)
        {
            _timeDifference += Time.deltaTime;
            if (_timeDifference >= TimeTillRestBonus)
            {
                _canRest = true;
                _timeDifference = 0;
                _restBonusActive = true;
                Debug.Log("RESTING");
            }
        }
        else if (_restBonusActive)
        {
            _timeDifference += Time.deltaTime;
            if(_timeDifference >= RestBonusLength)
            {
                _restBonusActive = false;
                _canRest = false;
                _canStartRest = false;
                _timeDifference = 0;
                Debug.Log("REST OVER");
            }
        }
    }

    public void OnButtonPress()
    {
        int score = 1;
        if(_restBonusActive)
        {
            score *= RestBonusMultiplier;
        }
        _score += score;
        scoreText.text = _score.ToString();
    }
}
