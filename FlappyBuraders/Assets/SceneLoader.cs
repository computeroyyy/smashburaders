﻿using UnityEngine;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    // Make sure the loading screen shows for at least 1 second:
    private const float MIN_TIME_TO_SHOW = 0.5f;
    // The reference to the current loading operation running in the background:
    private AsyncOperation currentLoadingOperation;
    // A flag to tell whether a scene is being loaded or not:
    private bool isLoading;
    // The rect transform of the bar fill game object:
    [SerializeField]
    private RectTransform barFillRectTransform;
    // Initialize as the initial local scale of the bar fill game object. Used to cache the Y-value (just in case):
    private Vector3 barFillLocalScale;
    // The text that shows how much has been loaded:
    [SerializeField]
    private Text percentLoadedText;
    // The elapsed time since the new scene started loading:
    private float timeElapsed;
    // Set to true to hide the progress bar:
    [SerializeField]
    private bool hideProgressBar;
    // Set to true to hide the percentage text:
    [SerializeField]
    private bool hidePercentageText;
    private void Awake()
    {
        // Singleton logic:
        if (Instance == null)
        {
            Instance = this;
            // Don't destroy the loading screen while switching scenes:
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Configure();
        // Hide();
    }
    void Start() {
        Show(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1));
    }
    private void Configure()
    {
        // Save the bar fill's initial local scale:
        barFillLocalScale = barFillRectTransform.localScale;
        // Enable/disable the progress bar based on configuration:
        barFillRectTransform.transform.parent.gameObject.SetActive(!hideProgressBar);
        // Enable/disable the percentage text based on configuration:
        percentLoadedText.gameObject.SetActive(!hidePercentageText);
    }
    private void Update()
    {
        if (isLoading)
        {
            // Get the progress and update the UI. Goes from 0 (start) to 1 (end):
            Debug.Log(currentLoadingOperation.progress);
            SetProgress(currentLoadingOperation.progress);
            // If the loading is complete, hide the loading screen:
            if (currentLoadingOperation.isDone)
            {
                Hide();
            }
            else
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= MIN_TIME_TO_SHOW)
                {
                    // The loading screen has been showing for the minimum time required.
                    // Allow the loading operation to formally finish:
                    currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }
    // Updates the UI based on the progress:
    private void SetProgress(float progress)
    {
        // Update the fill's scale based on how far the game has loaded:
        // barFillLocalScale.x = progress;
        // Update the rect transform:
        // barFillRectTransform.localScale = barFillLocalScale;
        // Set the percent loaded text:
        if (percentLoadedText)
            percentLoadedText.text = Mathf.CeilToInt(progress * 100).ToString() + "%";
    }
    // Call this to show the loading screen.
    // We can determine the loading's progress when needed from the AsyncOperation param:
    public void Show(AsyncOperation loadingOperation)
    {
        Debug.Log("Enable the loading screen:");
        // Enable the loading screen:
        gameObject.SetActive(true);
        // Store the reference:
        currentLoadingOperation = loadingOperation;
        // Stop the loading operation from finishing, even if it technically did:
        currentLoadingOperation.allowSceneActivation = false;
        // Reset the UI:
        SetProgress(0f);
        // Reset the time elapsed:
        timeElapsed = 0f;
        isLoading = true;
    }
    // Call this to hide it:
    public void Hide()
    {
        // Disable the loading screen:
        gameObject.SetActive(false);
        currentLoadingOperation = null;
        isLoading = false;
    }
}
