using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField]
    public Text PercentageText;

    [SerializeField]
    public Text CountDownText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one 'UIController'");
    }
}
