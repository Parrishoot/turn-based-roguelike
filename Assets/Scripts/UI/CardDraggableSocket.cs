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
    }

    public virtual void Hide() {
        backgroundImage.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic);

        if(socketText != null) {
            socketText.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic);            
        }
    }

    public abstract void ProcessCardInserted(CardUIController card);
}
