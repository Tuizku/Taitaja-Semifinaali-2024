using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour
{
    [SerializeField] private List<Spawn> Spawns = new();

    
    private void Start()
    {
        SpawnAll();
    }

    private void SpawnAll()
    {
        List<Vector2Int> takenPoses = new();

        foreach (Spawn spawn in Spawns)
        {
            for (int i = 0; i < spawn.SpawnAmount; i++)
            {
                Vector2Int pos = FindEmptyPos(spawn, takenPoses);
                takenPoses.Add(pos);
                SpawnSingleObject(spawn.Prefab, pos);
            }
        }
    }

    private void SpawnSingleObject(GameObject prefab, Vector2 prePos)
    {
        Vector3 worldPos = new Vector3(transform.position.x + prePos.x, transform.position.y + prePos.y);
        Instantiate(prefab, worldPos, Quaternion.identity);
    }

    private Vector2Int FindEmptyPos(Spawn spawn, List<Vector2Int> takenPoses)
    {
        while (true)
        {
            Vector2 floatPos = new Vector2(Random.Range(spawn.MinPos.x, spawn.MaxPos.x), Random.Range(spawn.MinPos.y, spawn.MaxPos.y));
            Vector2Int newPos = new Vector2Int(Mathf.RoundToInt(floatPos.x), Mathf.RoundToInt(floatPos.y));
            
            bool isEmpty = true;
            for (int i = 0; i < takenPoses.Count; i++)
            {
                if (takenPoses[i] == newPos) isEmpty = false;
            }
            if (isEmpty) return newPos;
        }
    }
}

[System.Serializable] // This spawns an amount of objects to a random position based on min and max
public class Spawn
{
    public GameObject Prefab;
    public Vector2 MinPos;
    public Vector2 MaxPos;
    public int SpawnAmount = 1;
}