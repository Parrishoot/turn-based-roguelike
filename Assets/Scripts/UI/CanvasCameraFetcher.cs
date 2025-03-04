using UnityEngine;

public class CanvasCameraFetcher : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();

        if(canvas != null) {
            canvas.worldCamera = Camera.main;
        }   
    }
}
