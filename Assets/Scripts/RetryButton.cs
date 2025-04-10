using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ScoreReset()
    {
        PlayerPrefs.DeleteKey($"{GameManager.instance.key}");
    }
}

