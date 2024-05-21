using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public static List<Entity> entities = new List<Entity>();
    [SerializeField] private UnityEvent onDie;
    [SerializeField] private UnityEvent<float> onDamage;
    public string entityTag;

    private Vector2Int[] neighbours =
    {
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(1, -1)
    };
    private const float smoothing = 0.05f;
    [HideInInspector] public Vector3 position;
    private Vector3 dampVel;

    [SerializeField] private int startingHealth = 10;
    private int health;
    public int Health
    {
        get { return health; }
        set {
            int nextHealth = Mathf.Max(value, 0);
            if (nextHealth == health) return; // If no changes would come

            health = nextHealth;
            onDamage.Invoke((float)health / startingHealth);

            if (health == 0) { // Call the death event
                onDie.Invoke();
            }
        }
    }

    public Sword sword;

    public void Start()
    {
        position = transform.position;
        Health = startingHealth;
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

    public void NewSword(Sword nextSword)
    {
        sword = nextSword;
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
            target.Health -= sword.Damage;
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
        bool isTileNeighbour = false;
        foreach (var n in neighbours)
        {
            if (currentPos + n == tile)
            {
                isTileNeighbour = true;
                break;
            }
        }
        if (isTileNeighbour) return true;
        return false;
    }
}
