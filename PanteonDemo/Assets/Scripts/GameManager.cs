using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AiObject;

    [SerializeField]
    private Transform AiParent;
    
    [SerializeField]
    private GameObject PlayerObject;

    [SerializeField]
    private List<Vector3> SpawnPoints;

    [SerializeField]
    [Tooltip("Max 10")]
    private int AiCount;

    public static GameManager Instance { get; private set; }
    public float CountDown;
    public bool HasGameStart;

    private PaintPercentageCalculator _paintPercentageCalc;
    private List<Transform> _players;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one 'GameManager'");
    }

    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        _paintPercentageCalc = FindObjectOfType<PaintPercentageCalculator>();
        _players = new List<Transform>();
        SpawnPlayers();
        OnStartAsync();
    }

    private async void OnStartAsync()
    {
        UIController.Instance.CountDownText.gameObject.SetActive(true);
        HasGameStart = false;

        while (CountDown > 0.25f)
            await Task.Yield();

        HasGameStart = true;
        UIController.Instance.CountDownText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (CountDown > 0)
        {
            CountDown -= Time.deltaTime;

            if (CountDown < 0.5f)
                UIController.Instance.CountDownText.text = "START";
            else
                UIController.Instance.CountDownText.text = CountDown.ToString("0");
        }

        if (_paintPercentageCalc.Percentage > 90f)
            UIController.Instance.SkipButton.SetActive(true);

        UIController.Instance.RankingText.text = String.Format("{0} / {1}", GetRankingPlace(), AiCount + 1);
    }

    private int GetRankingPlace()
    {
        var orderedList = _players.OrderByDescending(s => s.position.z).ToList();

        for (int i = 0; i < orderedList.Count; i++)
            if (orderedList[i].tag == "Player")
                return i + 1;

        return AiCount + 1;
    }

    private void SpawnPlayers()
    {
        var availableSpawnPoints = new List<Vector3>();

        foreach (var spawnPoint in SpawnPoints)
            availableSpawnPoints.Add(spawnPoint);

        for (int i = 0; i < AiCount; i++)
        {
            var rnd = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
            var bot = Instantiate(AiObject, availableSpawnPoints[rnd], Quaternion.identity, AiParent);
            _players.Add(bot.transform);
            availableSpawnPoints.Remove(availableSpawnPoints[rnd]);
        }

        PlayerObject.transform.position = availableSpawnPoints[0];
        _players.Add(PlayerObject.transform);
    }

    void OnDrawGizmosSelected()
    {
        foreach (var pos in SpawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 1);
        }
    }
}
