using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void PlayGame()
    {
        LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneWithDelay(int sceneBuildIndex)
    {
        StartCoroutine(PlayCoroutine(sceneBuildIndex));
    }

    public void LoadSceneWithDelay(string sceneName)
    {
        StartCoroutine(PlayCoroutine(sceneName));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        LoadSceneWithDelay("Title");
    }

    public void RestartLevel1()
    {
        LoadSceneWithDelay("Level 1");
    }

    public void RestartLevel2()
    {
        LoadSceneWithDelay("Level 2");
    }

    IEnumerator PlayCoroutine(int sceneBuildIndex)
    {
        // Print the time when the coroutine starts.
        Debug.Log("Started Coroutine at timestamp: " + Time.time);

        // Wait for 1 second.
        yield return new WaitForSeconds(0.9f);

        // Load the specified scene by build index.
        SceneManager.LoadScene(sceneBuildIndex);
    }

    IEnumerator PlayCoroutine(string sceneName)
    {
        // Print the time when the coroutine starts.
        Debug.Log("Started Coroutine at timestamp: " + Time.time);

        // Wait for 1 second.
        yield return new WaitForSeconds(1);

        // Load the specified scene by name.
        SceneManager.LoadScene(sceneName);
    }
}
