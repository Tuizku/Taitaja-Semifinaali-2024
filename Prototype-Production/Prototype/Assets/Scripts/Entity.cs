using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public static List<Entity> entities = new List<Entity>();
    [SerializeField] private UnityEvent onDie;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private int health;
    public string entityTag;

    private const float smoothing = 0.05f;
    [HideInInspector] public Vector3 position;
    private Vector3 dampVel;

    public int Health
    {
        get { return health; }
        set {
            int nextHealth = Mathf.Max(value, 0);
            if (nextHealth == health) return; // If no changes would come

            health = nextHealth;
            onDamage.Invoke();

            if (health == 0) { // Call the death event
                onDie.Invoke();
            }
        }
    }

    public int damage;

    private void Start()
    {
        position = transform.position;
    }
    private protected void OnEnable()
    {
        entities.Add(this);
    }
    private protected void OnDisable()
    {
        entities.Remove(this);
    }
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, position, ref dampVel, smoothing);
    }

    /// <summary>
    /// Moves the entity to another tile
    /// </summary>
    /// <param name="tile"></param>
    /// <returns>If the move was done</returns>
    public bool MoveTo(Vector2Int tile)
    {
        Entity target;
        if (GameManager.HasEntity(tile, out target)) // Attack
        {
            if (entityTag == target.entityTag) return false;
            target.Health -= damage;
            return false;
        }

        position = (Vector3Int)tile + new Vector3(0.5f, 0.5f);
        return true;
    }
    /// <summary>
    /// Check if a tile neighbours this entity
    /// </summary>
    /// <param name="tile">Target tile</param>
    /// <returns>If it is a neighbour</returns>
    public bool IsTileNeighbour(Vector2Int tile) {

        Vector2Int currentPos = GameManager.RoundToGrid(position);
        if (currentPos + new Vector2Int(1, 0) == tile) return true;
        if (currentPos + new Vector2Int(-1, 0) == tile) return true;
        if (currentPos + new Vector2Int(0, 1) == tile) return true;
        if (currentPos + new Vector2Int(0, -1) == tile) return true;
        return false;
    }
}
