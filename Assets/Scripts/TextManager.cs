using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TextManager
{
    public static void ToggleGoalText(bool toggle)
    {
        var goalText = GameObject.Find("GoalText").GetComponent<TextMeshProUGUI>();
        goalText.enabled = toggle;
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
    }
}
