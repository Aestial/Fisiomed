using UnityEngine;
using Fisiomed.Chat;
public class InteractiveActions : MonoBehaviour
{
    [SerializeField] bool destroyTarget;
    [SerializeField] GameObject target;
    public void Close()
    {
        if (destroyTarget)
            Destroy(target);
        else
            Destroy(gameObject);
    }    
}
