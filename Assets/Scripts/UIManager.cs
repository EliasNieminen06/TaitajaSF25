using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI score;
    void Update()
    {
        health.text = "HEALTH: " + PlayerController.Instance.health.ToString();
        score.text = "SCORE: " + PlayerController.Instance.score.ToString();
    }
}
