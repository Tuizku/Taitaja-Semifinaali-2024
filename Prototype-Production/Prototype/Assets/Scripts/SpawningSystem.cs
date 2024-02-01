using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour
{
    [SerializeField] private List<Spawn> Spawns = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable] // This spawns an amount of objects to a random position based on min and max
public class Spawn
{
    public GameObject Prefab;
    public Vector2 MinPos;
    public Vector2 MaxPos;
    public int SpawnAmount = 1;
    public bool UseDifficultyInAmount = true;
}