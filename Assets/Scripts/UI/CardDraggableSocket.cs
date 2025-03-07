using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardDraggableSocket : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;
    
    [SerializeField]
    private TMP_Text socketText;

    [SerializeField]
    private float fadeTime = .25f;

    public bool Insertable { get; private set; } = false;

    private void Start()
    {
        Color color = backgroundImage.color;
        color.a = 0f;

        backgroundImage.color = color;

        if(socketText != null) {
            socketText.color = color;
        }
    }

    public virtual void Show() {
        
        backgroundImage.DOFade(1f, fadeTime).SetEase(Ease.InOutCubic);

        if(socketText != null) {
            socketText.DOFade(1f, fadeTime).SetEase(Ease.InOutCubic);            
        }

        Insertable = true;
    }

    public virtual void Hide() {
        backgroundImage.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic);

        if(socketText != null) {
            socketText.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic);            
        }

        Insertable = false;
    }

    public abstract void ProcessCardInserted(CardUIController card);

    public abstract bool CanProcessCard(Card card);
}
