using System;
using UnityEngine;

public class CharacterDeathController : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    private void Start()
    {
        characterManager.Events.Death.OnNext(ProcessDeath);
    }

    private void ProcessDeath()
    {
        // TODO: Implement real death handling
        Destroy(gameObject);
    }
}
