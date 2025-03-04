using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardDraggableSocket : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private float fadeTime = .25f;

    private void Start()
    {
        Color color = backgroundImage.color;
        color.a = 0f;

        backgroundImage.color = color;
    }

    public void Show() {
        backgroundImage.DOFade(1f, fadeTime).SetEase(Ease.InOutCubic);
    }

    public void Hide() {
        backgroundImage.DOFade(0f, fadeTime).SetEase(Ease.InOutCubic);
    }

    public abstract void ProcessCardInserted(CardUIController card);
}
