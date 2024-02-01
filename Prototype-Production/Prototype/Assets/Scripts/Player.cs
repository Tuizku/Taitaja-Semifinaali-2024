using UnityEngine;
using UnityEngine.Events;

public class Player : Entity
{
    public static UnityAction<Vector2Int> onPlayerMove;
    public Vector2Int GetCursorTilePos()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return GameManager.RoundToGrid(mPos);
    }
    public void Move()
    {
        Vector2Int tilePos = GetCursorTilePos();

        if (!IsTileNeighbour(tilePos)) return;

        if (GameManager.HasWall(tilePos)) return;

        GameManager.DoTurn(); // Tell the enemies it's their turn

        onPlayerMove.Invoke(tilePos);
        if (!MoveTo(tilePos)) return;
    }
}
