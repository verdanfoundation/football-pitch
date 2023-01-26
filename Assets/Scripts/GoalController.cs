using TMPro;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [Header("GoalSettings")]
    public TeamColor team;
    public int goalCount;

    [HideInInspector] public int second;
    
    void Start()
    {
        second = (int)(1 / Time.deltaTime);
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
            TextManager.ColorGoalText(
                teamScoredGoal == TeamColor.Red ? new Color32(235, 64, 52, 255) : new Color32(66, 135, 245, 255));

            teamTMPro.text = goalCount.ToString();
        }
    }

    static TeamColor ReverseTeam(TeamColor originalTeam)
    {
        return originalTeam == TeamColor.Red ? TeamColor.Blue : TeamColor.Red;
    }
}
