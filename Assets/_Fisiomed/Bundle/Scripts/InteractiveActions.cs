using UnityEngine;
using Fisiomed.Chat;
public class InteractiveActions : MonoBehaviour
{
    [SerializeField] bool destroyObject;
    [SerializeField] GameObject target;
    public void Close()
    {
        if (destroyObject)
            Destroy(target);
        else
            Destroy(gameObject);
    }    
}
