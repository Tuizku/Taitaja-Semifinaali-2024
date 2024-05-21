using UnityEngine;

public class SummonerEnemy : Enemy
{
    private Vector2Int velocity;
    [SerializeField] private int steps = 3;
    [SerializeField] private Vector2Int[] possibleDirections;
    [SerializeField] private Object[] summonTargets;
    private int stepsSteppen;

    private void SetRandomVelocity()
    {
        velocity = possibleDirections[Random.Range(0, possibleDirections.Length)];
    }
    private void SummonAllys()
    {
        Instantiate(summonTargets[Random.Range(0, summonTargets.Length)], transform.position, Quaternion.identity);
    }
    private new void Start()
    {
        base.Start();
        SetRandomVelocity();
    }
    public override void Run()
    {
        if (stepsSteppen >= steps)
        {
            SetRandomVelocity();
            SummonAllys();
            stepsSteppen = 0;
            return;
        }

        Vector2Int rounded = GameManager.RoundToGrid(position);
        Vector2Int nextTile = rounded + velocity;
        stepsSteppen++;

        if (!GameManager.HasGround(nextTile) || GameManager.HasTagEntity(nextTile, "enemy")) return;

        MoveTo(nextTile);
    }
}
