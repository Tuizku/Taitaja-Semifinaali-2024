using UnityEngine;
using UnityEngine.Events;

public class EntityCollider : MonoBehaviour
{
    [SerializeField] private UnityEvent onDetect;
    Vector2Int pos;

    private void Start()
    {
        pos = GameManager.RoundToGrid(transform.position);
    }
    private void OnEnable()
    {
        Player.onPlayerMove += Detect;
    }
    private void OnDisable()
    {
        Player.onPlayerMove -= Detect;
    }
    private void Detect(Vector2Int playerMove)
    {
        if (playerMove == pos) onDetect.Invoke();
    }
}
