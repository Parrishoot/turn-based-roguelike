using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 direction;

    void Update()
    {
        transform.Rotate(direction.normalized * speed * Time.deltaTime);    
    }
}
