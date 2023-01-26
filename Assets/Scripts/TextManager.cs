using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    private static bool _isTextDisplayed;
    private static int _textDisplayFramesLeft;
    private static int _second;
    
    void Start()
    {
        _second = (int)(1 / Time.deltaTime);
    }

    void Update()
    {
        if (_isTextDisplayed && _textDisplayFramesLeft == 0)
        {
            ToggleGoalText(false);
            ToggleOutText(false);
            _isTextDisplayed = false;
        }
        
        if (_isTextDisplayed)
        {
            _textDisplayFramesLeft--;
        }
    }

    public static void ToggleGoalText(bool toggle)
    {
        var goalText = GameObject.Find("GoalText").GetComponent<TextMeshProUGUI>();
        goalText.enabled = toggle;
        _isTextDisplayed = true;
        _textDisplayFramesLeft = _second;
    }
    
    public static void ColorGoalText(Color color)
    {
        var goalText = GameObject.Find("GoalText").GetComponent<TextMeshProUGUI>();
        goalText.color = color;
    }

    public static void ToggleOutText(bool toggle)
    {
        var outText = GameObject.Find("OutText").GetComponent<TextMeshProUGUI>();
        outText.enabled = toggle;
        _isTextDisplayed = true;
        _textDisplayFramesLeft = _second;
    }
}
