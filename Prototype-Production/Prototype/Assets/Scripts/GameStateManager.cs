using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onNextMap;
    [SerializeField] private UnityEvent onEnemiesAlive;

    public void EndMap()
    {
        bool enemiesAlive = false;
        foreach (var e in Entity.entities)
        {
            if (e.entityTag == "enemy")
            {
                enemiesAlive = true;
                break;
            }
        }
        if (enemiesAlive)
        {
            onEnemiesAlive.Invoke();
            return;
        }
        onNextMap.Invoke();
    }
}
