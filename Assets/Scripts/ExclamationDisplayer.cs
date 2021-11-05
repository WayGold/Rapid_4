using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ExclamationDisplayer : MonoBehaviour
{
    SpriteRenderer _renderer;

    [SerializeField]
    int DisplayClickCount = 10;
    [SerializeField]
    float DisplayTime = 0.5f;
    [SerializeField]
    Sprite FastSprite;
    [SerializeField]
    Sprite SlowSprite;
    [SerializeField]
    Sprite GoodSprite;

    [SerializeField]
    Transform[] DrawPositions;


    int _currentClickCount;
    List<float> _inputTimings;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        _inputTimings = new List<float>();
    }

    public void OnClick()
    {
        _currentClickCount++;
        if(_currentClickCount >= DisplayClickCount)
        {
            _currentClickCount = 0;
            StartCoroutine("DisplaySprite");
        }
    }

    IEnumerator DisplaySprite()
    {
        int idx = Random.Range(0, DrawPositions.Length - 1);
        transform.position = DrawPositions[idx].position;
        transform.rotation = DrawPositions[idx].rotation;
        var avgTiming = CalculateAvgTiming();
        if(avgTiming <= -0.5)
        {
            _renderer.sprite = FastSprite;
        }
        else if (avgTiming >= 0.5)
        {
            _renderer.sprite = SlowSprite;
        }
        else
        {
            _renderer.sprite = GoodSprite;
        }
        _renderer.enabled = true;
        yield return new WaitForSeconds(DisplayTime);
        _renderer.enabled = false;
    }

    float CalculateAvgTiming()
    {
        float avg = 0;
        foreach(var item in _inputTimings)
        {
            avg += item;
        }
        avg /= _inputTimings.Count;
        _inputTimings.Clear();
        return avg;
    }

    public void InputTimingRecieved(int timing)
    {
        _inputTimings.Add(timing);
    }
}
