using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimationController : MonoBehaviour
{
    public bool AnimationInProgress { get; private set; } = false;
    
    [SerializeField]
    private RectTransform cardPanelTransform;

    [SerializeField]
    private RectTransform cardHeaderTransform;

    [SerializeField]
    private RectTransform cardBaseTransform;

    private Material cardFooterMat;

    private bool passiveColor = false;

    void Start()
    {
        Image cardFooterImage = cardBaseTransform.GetComponent<Image>();

        if(cardFooterImage == null) {
            Debug.LogWarning("Can't get card footer image material");
            return;
        }

        cardFooterMat = Instantiate(cardFooterImage.material);
        cardFooterImage.material = cardFooterMat;
    }

    public void Flip(Action OnCardFlipped) {

        AnimationInProgress = true;

        float splitAmount = 20f;
        float sequencePieceTime = .25f;

        DOTween.Sequence()
        .Append(cardBaseTransform.DOLocalMoveY(cardBaseTransform.anchoredPosition.y - splitAmount, sequencePieceTime).SetEase(Ease.InOutElastic))
        .Join(cardHeaderTransform.DOLocalMoveY(cardHeaderTransform.anchoredPosition.y + splitAmount, sequencePieceTime).SetEase(Ease.InOutElastic))
        .Append(cardBaseTransform.DOLocalRotate(Vector3.up * 90, sequencePieceTime / 2).SetEase(Ease.InCubic).OnComplete(() => {
            passiveColor = !passiveColor;
            cardFooterMat.SetInt("_Passive", passiveColor ? 1 : 0);

            OnCardFlipped?.Invoke();
        }))
        .Append(cardBaseTransform.DOLocalRotate(Vector3.zero, sequencePieceTime / 2).SetEase(Ease.OutCubic))
        .Append(cardBaseTransform.DOLocalMoveY(cardBaseTransform.anchoredPosition.y, sequencePieceTime).SetEase(Ease.InOutElastic))
        .Join(cardHeaderTransform.DOLocalMoveY(cardHeaderTransform.anchoredPosition.y, sequencePieceTime).SetEase(Ease.InOutElastic))
        .Play()
        .OnComplete(() => {
            AnimationInProgress = false;
        });
    }

    public void Shrink() {
        cardPanelTransform.DOScale(Vector3.one * .8f, .125f).SetEase(Ease.InOutCubic);
    }

    public void Grow() {
        cardPanelTransform.DOScale(Vector3.one, .125f).SetEase(Ease.InOutCubic);
    }
}
