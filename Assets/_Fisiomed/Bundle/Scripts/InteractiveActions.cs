using UnityEngine;
using Fisiomed.Chat;
public class InteractiveActions : MonoBehaviour
{
    [SerializeField] bool destroyObject = default;
    [SerializeField] GameObject target = default;
    public void Close()
    {
        if (destroyObject)
            Destroy(target);
        else
            Destroy(gameObject);
    }    
}
