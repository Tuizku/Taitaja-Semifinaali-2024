using UnityEngine;

public class AngleEnemy : Enemy
{
    int angle = 0;

    private Vector2 CalculateMoveWithAngle(int angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.down;
    }
    public override void Run()
    {
        Vector2Int rounded = GameManager.RoundToGrid(position);
        Vector2 move = CalculateMoveWithAngle(angle);
        Vector2Int nextTile = GameManager.RoundToGrid(move) + rounded;

        while (!GameManager.HasGround(nextTile) || GameManager.HasTagEntity(nextTile, "enemy"))
        {
            angle += 90;

            // Recalculate the move
            move = CalculateMoveWithAngle(angle);
            nextTile = GameManager.RoundToGrid(move) + rounded;
        }
        MoveTo(nextTile);
    }
}
