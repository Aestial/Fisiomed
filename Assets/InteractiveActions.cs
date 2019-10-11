using UnityEngine;
using Fisiomed.Chat;
public class InteractiveActions : MonoBehaviour
{
    public void Close()
    {
        ChatController.Instance.NextBubble();
        Destroy(gameObject);
    }    
}
