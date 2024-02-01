using UnityEngine;

// Creates object on death
// Used for adding death particles
public class DeathInstancer : MonoBehaviour
{
    [SerializeField] private Object instance;
    [SerializeField] private float instanceAliveTime = 0.25f;

    public void Die()
    {
        if (instance != null)
        {
            GameObject newInstance = Instantiate(instance, transform.position, transform.rotation) as GameObject;
            Destroy(newInstance, instanceAliveTime);
        }
        
        Destroy(gameObject);
    }
}
