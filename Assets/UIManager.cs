using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    void Update()
    {
        health.text = "HEALTH: " + PlayerController.Instance.health.ToString();
    }
}
