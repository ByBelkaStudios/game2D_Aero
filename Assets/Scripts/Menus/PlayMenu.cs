using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void LoadPlayMenuScene()
    {
        StartCoroutine(LoadScene());
    }

    private System.Collections.IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
