using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIController : MonoBehaviour, IPointerClickHandler
{

    private const float IDLE_ROTATE_AMOUNT = 2f; 
    private const float IDLE_BOB_AMOUNT = 2f;
    private const float IDLE_ANIMATION_TIME = 2f;

    private const float ROTATE_TIME = .5f; 

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private RectTransform cardPanelTransform;

    private Card card;

    private PassiveController passive = null;

    private Tween activeFlipAnimation = null;

    private bool frontShowing = true;

    public void Setup(Card card)
    {
        this.card = card;
        titleText.text = card.CardName;
        descriptionText.text = card.Active.GetAbilityDescription();

        BeginAnimation();
    }

    public void Flip() {

        if(activeFlipAnimation != null || PassiveActive()) {
            return;
        }

        
        frontShowing = !frontShowing;

        activeFlipAnimation = DOTween.Sequence()
            .Append(cardPanelTransform.DORotate(Vector3.up * 90, ROTATE_TIME / 2).SetEase(Ease.InCubic).OnComplete(UpdateFlippingCard))
            .Append(cardPanelTransform.DORotate(Vector3.zero, ROTATE_TIME / 2).SetEase(Ease.OutCubic))
            .Play()
            .OnComplete(() => {
                activeFlipAnimation = null;
            });

    }

    private void ShowPassiveActive() {
        transform.DOScale(Vector3.one * .8f, ROTATE_TIME / 2).SetEase(Ease.InOutCubic);
    }

    private void UpdateFlippingCard() {
        cardPanelTransform.eulerAngles = Vector3.up * -90f;
        descriptionText.text = frontShowing ? card.Active.GetAbilityDescription() : card.Passive.GetAbilityDescription();
    }

    public void Use() {
        if(frontShowing) {
            UseActive();
        }
        else {
            TogglePassive();
        }
    }

    public void UseActive() {

        if(PassiveActive()) {
            return;
        }

        PlayerCharacterManager playerCharacterManager = FindAnyObjectByType<PlayerCharacterManager>();

        AbilityProcessor abilityProcessor = card.Active.GetAbilityProcessor(playerCharacterManager);
        abilityProcessor.OnAbilityFinish += DiscardCard;

        abilityProcessor.Process();
    }

    public void TogglePassive() {

        if(!PassiveActive()) {
            passive = card.Passive.GetController();
            passive.Activate();
            ShowPassiveActive();
        }
        else {
            passive.Deactivate();
            DiscardCard();
        }
    }

    private void DiscardCard()
    {
        DeckManager.Instance.DiscardAbility(card);
        Destroy(gameObject);
    }

    private bool PassiveActive() {
        return passive != null && passive.Active;
    }

    private void BeginAnimation() {

        transform.eulerAngles = Vector3.forward * IDLE_ROTATE_AMOUNT;


        // Rotate idle animation
        DOTween.Sequence()
        .Append(transform.DORotate(Vector3.forward * -IDLE_ROTATE_AMOUNT, IDLE_ANIMATION_TIME).SetEase(Ease.InOutSine))
        .SetLoops(-1, LoopType.Yoyo)
        .Play();

        // Bob idle animation
        DOTween.Sequence()
        .Append(cardPanelTransform.DOAnchorPosY(cardPanelTransform.anchoredPosition.y + IDLE_BOB_AMOUNT, IDLE_ANIMATION_TIME * .8f).SetEase(Ease.InOutSine))
        .SetLoops(-1, LoopType.Yoyo)
        .Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left) {
            Use();
        }
        else if(eventData.button == PointerEventData.InputButton.Right) {
            Flip();
        }
    }
}