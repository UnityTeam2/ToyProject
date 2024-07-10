using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public Button toggleButton;
    private Text buttonText;

    private bool isGameRunning = false;

    void Start()
    {

        Time.timeScale = 0f;

        if (toggleButton != null)
        {
            buttonText = toggleButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Start Game";
            }

            toggleButton.onClick.AddListener(OnToggleButtonClick);
        }
        else
        {
            Debug.LogError("Toggle Button is not assigned.");
        }
    }

    void OnToggleButtonClick()
    {
        if (isGameRunning)
        {
            StopGame();
        }
        else
        {
            StartGame();
        }
    }

    void StartGame()
    {

        Time.timeScale = 1f;
        isGameRunning = true;
        if (buttonText != null)
        {
            buttonText.text = "Stop Game";
        }
    }

    void StopGame()
    {

        Time.timeScale = 0f;
        isGameRunning = false;
        if (buttonText != null)
        {
            buttonText.text = "Start Game";
        }
    }
}


