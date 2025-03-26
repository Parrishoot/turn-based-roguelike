using System;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]
    private HealthController healthController;

    void Start()
    {
        healthController.OnDamageTaken += Shake;
    }

    private void Shake(int damage)
    {
        CameraController.Instance.Shake(new CameraShakeAttributes());
    }
}
