using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ButtonScoreManager : MonoBehaviour
{
    [SerializeField, Tooltip("The Multiplier that the player recieves when the rest bonus is active")]
    int RestBonusMultiplier;
    [SerializeField, Tooltip("The Multiplier that the player recieves when the flow bonus is active")]
    int FlowBonusMultiplier;
    [SerializeField, Tooltip("Ammount of time the player has their rest bonus active when working")]
    float RestBonusLength;
    [SerializeField, Tooltip("Ammount of time the player must rest before the rest bonus can be earned")]
    float RequiredRestTime;
    [SerializeField, Tooltip("Ammount of time the player must work before the rest bonus can be earned")]
    float RequiredWorkTime;
    [SerializeField, Tooltip("Ammount of time the player to not trigger fatigue punishment with 10 taps")]
    float RequiredFatigueTriggerTime;
    [SerializeField, Tooltip("+- This range to RequiredFlowTapSec to determine flow bonus")]
    float RequiredFlowRange;
    [SerializeField, Tooltip("Ammount of delta tapping time the player need to maintain in order to get bonus")]
    float RequiredFlowTapSec;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text fatigueText;
    [SerializeField]
    Material characterMaterial;

    public UnityEvent<int> ClickFlowEvent;

    bool _canRest = false;              // represents if the player has worked long enough to earn rest bonus
    bool _restBonusEarned = false;      // represents if the player has rested long eouugh to earn rest bonus
    bool _resting = false;              // represents whether the player is currently resting
    bool _flowBonusEarned = false;
    bool _didJustTap = false;

    float _timeDifference = 0;
    int _score = 0;

    int _fatigueVal = 50;
    int _maxFatigue = 100;

    int tapTracker = 0;
    int flowTracker = 0;
    float _timeSinceFirstTap = 0;
    float _timeSinceLastTap = 0;

    public float GetFatigue() { return _fatigueVal; }
    public float GetScore() { return _score; }
    public bool GetState() { return _resting; }

    public Animator animator;
    public Animator animator2;
    

    // Start is called before the first frame update
    void Start()
    {
        if(characterMaterial != null)
            characterMaterial.SetFloat("Vector1_8fa194bf8cc749bdacb7da5ab0ade932", (float)(_fatigueVal / _maxFatigue));

        GameObject MainChar = GameObject.Find("P4_MainChar");
        GameObject Chair = GameObject.Find("P4_ChairColor");
        if (MainChar != null)
        {
            animator = MainChar.GetComponent<Animator>(); 
            animator2 = Chair.GetComponent<Animator>();
        }

        if(ClickFlowEvent == null)
        {
            ClickFlowEvent = new UnityEvent<int>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_timeSinceLastTap >= 1.5f)
        {
            if (_didJustTap)
            {
                animator.SetBool("isWorking", true);
                _didJustTap = false;
            }
            else
            {
                animator.SetBool("isWorking", false);
            }

            if (_resting)
            {
                animator.SetBool("isResting", true);
                new WaitForSeconds(5f);
                animator2.SetBool("isResting", true);
            }
            else
            {
                animator.SetBool("isResting", false);
                new WaitForSeconds(5f);
                animator2.SetBool("isResting", false);
            }
                   
            
        }
        if(_resting)
        {
            
            if (_canRest && !_restBonusEarned)
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
        if(tapTracker >= 10){
            //Debug.Log("Time Since First Tap: " + _timeSinceFirstTap);
            if(_timeSinceFirstTap <= RequiredFatigueTriggerTime){
                _fatigueVal++;
                fatigueText.text = _fatigueVal.ToString();
            }
            tapTracker = 0;
            _timeSinceFirstTap = 0;
        }
        else{
            _timeSinceFirstTap += Time.deltaTime;
        }
        
        // Flow Bonus Section, check delta time between every tap
        if(flowTracker != 0){
           // Debug.Log("Time Since Last Tap: " + _timeSinceLastTap);
            // Check whether the delta time of two taps is in range
            if(_timeSinceLastTap >= RequiredFlowTapSec - RequiredFlowRange && 
                _timeSinceLastTap <= RequiredFlowTapSec + RequiredFlowRange){
                Debug.Log("FLOW BONUS ACTIVE!");
                // Set bonus flag to true and reset timer
                _flowBonusEarned = true;
                _timeSinceLastTap = 0;
            }
            else{
                // Turn off bonus flag if not in range
                _flowBonusEarned = false;
            }
            // Reset Tracker and timer
            flowTracker = 0;
            _timeSinceLastTap = 0;
        }
        else{
            // Calc The Time Past Since Last Tap
            _timeSinceLastTap += Time.deltaTime;
        }

        if(characterMaterial != null)
            characterMaterial.SetFloat("Vector1_8fa194bf8cc749bdacb7da5ab0ade932", (float)(_fatigueVal / _maxFatigue));

        // Force Rest When Fatigue Level Reaches Max
        if(_fatigueVal >= _maxFatigue){
            Debug.Log("Fatigue Max! Force Rest!");
            ToggleResting();
        }

    
    }



    private void DecrementFatigueLvl(){
        if(_fatigueVal >= 50)
                _fatigueVal = _fatigueVal - 50; 
            else{
                _fatigueVal = 0;
            }
    }


   
    public void ToggleResting()
    {
        if(!_resting){
            DecrementFatigueLvl();
            
        }
        _resting = !_resting;
    }

  

  
    public void OnButtonPress() 
    {
        _didJustTap = true;
        //animator.speed *= 1.1f;
        animator.SetBool("isWorking", true);
        if (!_resting)
        {
            int TapResult = 0;
            if(_timeSinceLastTap < RequiredFlowTapSec - RequiredFlowRange)
            {
                TapResult = -1;
                Debug.Log("TOO FAST");
            }
            else if (_timeSinceLastTap > RequiredFlowTapSec + RequiredFlowRange)
            {
                TapResult = 1;
                Debug.Log("TOO SLOW");
            }
            else
            {
                Debug.Log("GREAT");
            }

            int score = 1;
            if(_restBonusEarned)
            {
                score *= RestBonusMultiplier;
            }
            else if(_flowBonusEarned){
                score *= FlowBonusMultiplier;
            }
            else if(_restBonusEarned && _flowBonusEarned){
                score = score * RestBonusMultiplier * FlowBonusMultiplier;
            }
            _score += score;
            scoreText.text = _score.ToString();
            
            tapTracker++;
            flowTracker++;

            ClickFlowEvent.Invoke(TapResult);
        }
    }
    
 
}
