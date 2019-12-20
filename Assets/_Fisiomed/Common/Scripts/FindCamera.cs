using UnityEngine;

public class FindCamera : MonoBehaviour
{
    new Camera camera;
    Canvas canvas;    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        camera = Camera.main;
        canvas.worldCamera = camera;
    }   
}
