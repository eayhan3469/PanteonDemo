using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField]
    public Text PercentageText;

    [SerializeField]
    public Text CountDownText;

    [SerializeField]
    public Text RankingText;

    [SerializeField]
    public GameObject GameOverPanel;

    [SerializeField]
    public GameObject SkipButton;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one 'UIController'");
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickSkip()
    {
        SkipButton.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
