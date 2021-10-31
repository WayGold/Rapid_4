using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    float _gameTime;
    float _timePassed;
    float _workEndOfDay;

    bool _initialUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
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
            OnTimeUpdate.Invoke(_gameTime);
        }
    }
}
