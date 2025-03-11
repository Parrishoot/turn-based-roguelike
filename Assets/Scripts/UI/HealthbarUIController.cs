using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUIController : MonoBehaviour
{
    private const string SHIELD_PERCENT = "_ShieldPercent";
    private const string HEALTH_PERCENT = "_HealthPercent";

    [SerializeField]
    private HealthController healthController;

    [SerializeField]
    private Image background;

    [SerializeField]
    private TMP_Text text;

    private Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = Instantiate(background.material);
        background.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        if(text != null) {
            text.text = string.Format("{0} / {1} (+{2})", healthController.CurrentHealth, healthController.MaxHealth, healthController.CurrentShield);
        }

        material.SetFloat(SHIELD_PERCENT, (float) healthController.CurrentShield / healthController.MaxHealth);
        material.SetFloat(HEALTH_PERCENT, (float) healthController.CurrentHealth / healthController.MaxHealth);
    }
}
