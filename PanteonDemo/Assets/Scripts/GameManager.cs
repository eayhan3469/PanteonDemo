using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one 'GameManager'");
    }

    void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        var availableSpawnPoints = new List<Vector3>();

        foreach (var spawnPoint in SpawnPoints)
            availableSpawnPoints.Add(spawnPoint);

        for (int i = 0; i < AiCount; i++)
            Instantiate(AiObject, availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)], Quaternion.identity, AiParent);

        //Instantiate(PlayerObject, availableSpawnPoints[0], Quaternion.identity);
    }

    public void Respawn(GameObject aiObject)
    {
        Destroy(aiObject);
        Instantiate(AiObject, SpawnPoints[Random.Range(0, SpawnPoints.Count)], Quaternion.identity, AiParent);
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
