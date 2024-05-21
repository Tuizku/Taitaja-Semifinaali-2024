using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static UnityAction onTurn; // For broadcasting next turn
    public static GameObject Player;

    [SerializeField] private Tilemap wallMap;
    [SerializeField] private Tilemap groundMap;

    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Simulate one turn on the board
    /// </summary>
    public static void DoTurn()
    {
        if (onTurn != null) onTurn.Invoke(); // Don't bug out when no one is listening
    }
    /// <summary>
    /// Used to round entity positions, and other non interger position to grid
    /// </summary>
    /// <param name="pos">Float position</param>
    /// <returns>Integer position</returns>
    public static Vector2Int RoundToGrid(Vector2 pos)
    {
        return (Vector2Int)Instance.wallMap.LocalToCell(pos);
    }
    /// <summary>
    /// Return true if the position has any tile
    /// </summary>
    /// <returns>True if it has a wall</returns>
    public static bool HasWall(Vector2Int pos)
    {
        return Instance.wallMap.HasTile((Vector3Int)pos);
    }
    /// <summary>
    /// Return true if the position has any tile
    /// </summary>
    /// <returns>True if it has a wall</returns>
    public static bool HasGround(Vector2Int pos)
    {
        return Instance.groundMap.HasTile((Vector3Int)pos);
    }
    /// <summary>
    /// Find out if a spot has an entity
    /// </summary>
    /// <param name="tile">The spot</param>
    /// <returns>If the spot has an entity</returns>
    public static bool HasEntity(Vector2Int tile, out Entity target)
    {
        bool entityInSpot = false;
        Entity newTarget = null;
        foreach (var entity in Entity.entities)
        {
            Vector2Int roundedPos = RoundToGrid(entity.position);
            if (tile != roundedPos) continue;

            entityInSpot = true;
            newTarget = entity;
            break;
        }
        target = newTarget;
        return entityInSpot;
    }
}
