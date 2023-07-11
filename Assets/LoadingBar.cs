using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public Slider slider;
    private string sceneName = "MainGame";

    private float sceneLoadStartTime;
    private float sceneLoadEndTime;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Start measuring the loading time
        sceneLoadStartTime = Time.realtimeSinceStartup;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Calculate the progress based on the async operation progress
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            UpdateSliderValue(progress);
            yield return null;
        }

        // Measure the loading time once the scene is fully loaded
        sceneLoadEndTime = Time.realtimeSinceStartup;

        // Calculate the actual loading time
        float loadingTime = sceneLoadEndTime - sceneLoadStartTime;

        Debug.Log("Scene loaded in " + loadingTime + " seconds");
    }

    private void UpdateSliderValue(float progress)
    {
        slider.value = progress;
    }
}
