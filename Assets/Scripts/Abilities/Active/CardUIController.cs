using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIController : MonoBehaviour, IPointerClickHandler
{

    private const float IDLE_ROTATE_AMOUNT = 2f; 
    private const float IDLE_BOB_AMOUNT = 2f;
    private const float IDLE_ANIMATION_TIME = 2f;

    private const float ROTATE_TIME = .25f; 

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private RectTransform cardPanelTransform;

    [SerializeField]
    private Draggable cardDraggable;

    public Card Card { get; private set; }

    private PassiveController passive = null;

    private Tween activeFlipAnimation = null;

    private bool frontShowing = true;

    public void Setup(Card card)
    {
        this.Card = card;
        titleText.text = card.CardName;
        descriptionText.text = card.Active.GetAbilityDescription();

        BeginAnimation();
    }

    public void Flip() {

        if(activeFlipAnimation != null || PassiveActive()) {
            return;
        }

        
        frontShowing = !frontShowing;
        cardDraggable.enabled = frontShowing;

        activeFlipAnimation = DOTween.Sequence()
            .Append(cardPanelTransform.DORotate(Vector3.up * 90, ROTATE_TIME / 2).SetEase(Ease.InCubic).OnComplete(UpdateFlippingCard))
            .Append(cardPanelTransform.DORotate(Vector3.zero, ROTATE_TIME / 2).SetEase(Ease.OutCubic))
            .Play()
            .OnComplete(() => {
                activeFlipAnimation = null;
            });

    }

    private void ShowPassiveActive() {
       
    }

    private void UpdateFlippingCard() {
        cardPanelTransform.eulerAngles = Vector3.up * -90f;
        descriptionText.text = frontShowing ? Card.Active.GetAbilityDescription() : Card.Passive.GetAbilityDescription();
    }

    public void Use() {
        if(frontShowing) {
            return;
        }
        else {
            TogglePassive();
        }
    }

    public void UseActive(CharacterManager characterManager) {

        if(PassiveActive()) {
            return;
        }

        AbilityProcessor abilityProcessor = Card.Active.GetAbilityProcessor(characterManager);

        abilityProcessor.Process();
        DiscardCard();
    }

    public void TogglePassive() {

        if(!PassiveActive()) {
            passive = Card.Passive.GetController();
            passive.Activate();
            Shrink();
        }
        else {
            passive.Deactivate();
            DiscardCard();
        }
    }

    private void DiscardCard()
    {
        DeckManager.Instance.DiscardAbility(Card);
        Destroy(gameObject);
    }

    private bool PassiveActive() {
        return passive != null && passive.Active;
    }

    private void BeginAnimation() {

        transform.eulerAngles = Vector3.forward * IDLE_ROTATE_AMOUNT;

        // Rotate idle animation
        DOTween.Sequence()
        .Append(cardPanelTransform.DORotate(Vector3.forward * -IDLE_ROTATE_AMOUNT, IDLE_ANIMATION_TIME).SetEase(Ease.InOutSine))
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

    public void Shrink() {
        cardPanelTransform.DOScale(Vector3.one * .5f, ROTATE_TIME / 2).SetEase(Ease.InOutCubic);
    }

    public void Grow() {
        cardPanelTransform.DOScale(Vector3.one, ROTATE_TIME / 2).SetEase(Ease.InOutCubic);
    }
}