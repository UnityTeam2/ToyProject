using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public Button startButton;
    public Button stopButton;

    void Start()
    {
        // ó�� ������ �� ������ �Ͻ� ���� ���·� ����
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
        // ������ ��� ���·� ��ȯ
        Time.timeScale = 1f;
    }

    void StopGame()
    {
        // ������ �Ͻ� ���� ���·� ��ȯ
        Time.timeScale = 0f;
    }
}


