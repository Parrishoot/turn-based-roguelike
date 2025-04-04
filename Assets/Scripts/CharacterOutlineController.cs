using UnityEngine;

public class CharacterOutlineController : MonoBehaviour
{
    [SerializeField]
    private Outline outline;

    [SerializeField]
    private CharacterManager characterManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline.enabled = false;

        characterManager.OnSpaceHoverStart.OnEvery(() => outline.enabled = true);
        characterManager.OnSpaceHoverEnd.OnEvery(() => outline.enabled = false);
    }
}
