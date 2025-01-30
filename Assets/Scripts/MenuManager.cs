using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play(){
        AudioManager.Instance.Button();
        SceneManager.LoadScene("GameScene");
    }
}
