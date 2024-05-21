using UnityEngine;

public class RowEnemy : Enemy
{
    [SerializeField] private Vector2Int velocity;
    public override void Run()
    {
        Vector2Int rounded = GameManager.RoundToGrid(position);
        Vector2Int nextTile = rounded + velocity;
        if (!GameManager.HasGround(nextTile)) {
            velocity *= -1;
            nextTile = rounded + velocity;
        }
        
        MoveTo(nextTile);
    }
}
