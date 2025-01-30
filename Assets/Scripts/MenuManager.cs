using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI score;

void Update(){
    score.text = "HIGHSCORE: " + PlayerPrefs.GetInt("score").ToString();
}

    public void Play(){
        AudioManager.Instance.Button();
        SceneManager.LoadScene("GameScene");
    }
}
