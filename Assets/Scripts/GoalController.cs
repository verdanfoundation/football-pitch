using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [Header("GoalSettings")]
    public TeamColor team;
    public int goalCount = 0;
    private int _framesToHideGoalText = 1;
    private bool _isGoalTextEnabled;

    [HideInInspector] public int second;
    
    void Start()
    {
        second = (int)(1 / Time.deltaTime);
    }
    
    void Update()
    {
        if (_isGoalTextEnabled)
            _framesToHideGoalText--;

        if (_framesToHideGoalText == 0 && _isGoalTextEnabled)
        {
            TextManager.ToggleGoalText(false);
            _isGoalTextEnabled = false;
        }
    }
    
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Ball"))
        {
            goalCount++;
            var teamScoredGoal = ReverseTeam(team);
            var teamScoreControllerName = teamScoredGoal + "Score";

            var teamScoreController = GameObject.Find(teamScoreControllerName);
            var teamTMPro = teamScoreController.GetComponent<TextMeshProUGUI>();

            TextManager.ToggleGoalText(true);
            _isGoalTextEnabled = true;
            TextManager.ColorGoalText(
                teamScoredGoal == TeamColor.Red ? new Color32(235, 64, 52, 255) : new Color32(66, 135, 245, 255));

            _framesToHideGoalText = second;

            teamTMPro.text = goalCount.ToString();
        }
    }

    static TeamColor ReverseTeam(TeamColor originalTeam)
    {
        return originalTeam == TeamColor.Red ? TeamColor.Blue : TeamColor.Red;
    }
}
