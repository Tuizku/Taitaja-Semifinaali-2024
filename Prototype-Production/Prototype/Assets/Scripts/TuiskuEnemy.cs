using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuiskuEnemy : Enemy
{
    [SerializeField] [Range(0f, 1f)] private float MoveRarity = 0.15f;
    [SerializeField] private GameObject Player;

    public override void Run()
    {
        if (Player == null) Player = GameManager.Player;

        float randomFloat = Random.Range(0f, 1f);
        if (randomFloat < MoveRarity) return;

        Vector2Int rounded = GameManager.RoundToGrid(position);
        Vector2Int targetRounded = GameManager.RoundToGrid(Player.transform.position);
        Vector2Int distance = targetRounded - rounded;
        Vector2Int nextTile = rounded + new Vector2Int(Mathf.Clamp(distance.x, -1, 1), Mathf.Clamp(distance.y, -1, 1));

        //Debug.Log($"Current pos: {rounded} | Player pos: {targetRounded} | Distance: {distance} | Next: {nextTile}  ||  random: {randomFloat}");

        if (GameManager.HasGround(nextTile)) MoveTo(nextTile);
    }
}
