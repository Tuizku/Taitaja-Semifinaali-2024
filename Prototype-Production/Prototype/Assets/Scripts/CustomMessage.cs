using UnityEngine;

public class CustomMessage : MonoBehaviour
{
    public void SendPlayerMessage(string text)
    {
        PlayerMessager.instance.SendPlayerMessage(text);
    }
}
