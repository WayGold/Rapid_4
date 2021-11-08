using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchEvent : MonoBehaviour
{
    [SerializeField]
    ButtonScoreManager mgr;
    public UnityEvent OnTouch;

    void Start()
    {
        if(OnTouch == null)
        { 
            OnTouch = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!mgr.GetState())
        {
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    OnTouch.Invoke();
                }
            }
    #if DEBUG
            if (Input.GetMouseButtonDown(0))
            {
                OnTouch.Invoke();
            }
    #endif
        }
    }
}
