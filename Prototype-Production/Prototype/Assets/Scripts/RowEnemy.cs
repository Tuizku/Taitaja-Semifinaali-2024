using UnityEngine;

public class RowEnemy : Enemy
{
    [SerializeField] private Vector2Int velocity;
    public override void Run()
    {
        Vector2Int rounded = GameManager.RoundToGrid(position);
        Vector2Int nextTile = rounded + velocity;
        if (!GameManager.HasGround(nextTile) || GameManager.HasTagEntity(nextTile, "enemy")) {
            velocity *= -1;
            return;
        }
        
        MoveTo(nextTile);
    }
}
