using UnityEngine;
using UnityEngine.Events;

public class EntityCollider : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private UnityEvent onDetect;

    private void OnEnable()
    {
        GameManager.onTurn += Detect;
    }
    private void OnDisable()
    {
        GameManager.onTurn -= Detect;
    }
    private void Detect()
    {
        foreach (var entity in Entity.entities)
        {
            Vector2Int pos = GameManager.RoundToGrid(transform.position);
            Vector2Int entityPos = GameManager.RoundToGrid(entity.position);
            Debug.Log(pos == entityPos);
            if (entityPos == pos && entity.entityTag == targetTag) onDetect.Invoke();
        }
    }
}
