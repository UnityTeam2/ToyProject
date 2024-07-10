using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public Button startButton;
    public Button stopButton;

    void Start()
    {
        // 처음 시작할 때 게임을 일시 정지 상태로 설정
        Time.timeScale = 0f;

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }
        else
        {
            Debug.LogError("Start Button is not assigned.");
        }

        if (stopButton != null)
        {
            stopButton.onClick.AddListener(OnStopButtonClick);
        }
        else
        {
            Debug.LogError("Stop Button is not assigned.");
        }
    }

    void OnStartButtonClick()
    {
        StartGame();
    }

    void OnStopButtonClick()
    {
        StopGame();
    }

    void StartGame()
    {
        // 게임을 재생 상태로 전환
        Time.timeScale = 1f;
    }

    void StopGame()
    {
        // 게임을 일시 정지 상태로 전환
        Time.timeScale = 0f;
    }
}


