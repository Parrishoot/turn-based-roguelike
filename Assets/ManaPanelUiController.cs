using TMPro;
using UnityEngine;

public class ManaPanelUiController : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    private TMP_Text currentManaText;

    private ManaGer manaGer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manaGer = ManaGer.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        currentManaText.text = manaGer.CurrentMana.ToString();
    }
}
