using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityAction onTurn; // For broadcasting next turn
    private static Tilemap map;

    private void Awake()
    {
        map = GetComponentInChildren<Tilemap>(); // Find the tilemap
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
        return (Vector2Int)map.LocalToCell(pos);
    }
    /// <summary>
    /// Return if the position has any tile
    /// </summary>
    /// <param name="tile">The position</param>
    /// <returns>If it has a wall</returns>
    public static bool HasWall(Vector2Int tile)
    {
        return map.HasTile((Vector3Int)tile);
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
