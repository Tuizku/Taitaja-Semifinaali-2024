using UnityEngine;
using UnityEngine.Events;

public class PlayerMessager : MonoBehaviour
{
    public static PlayerMessager instance;
    [SerializeField] private UnityEvent onShowMessage;
    [SerializeField] private UnityEvent<string> onWriteMessage;

    private void Awake()
    {
        instance = this;
    }
    public void SendPlayerMessage(string text)
    {
        onWriteMessage.Invoke(text);
        onShowMessage.Invoke();
    }
}
