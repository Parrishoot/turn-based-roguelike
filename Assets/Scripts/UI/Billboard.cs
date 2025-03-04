using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Start()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
