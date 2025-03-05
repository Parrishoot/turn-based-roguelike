using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIController : Draggable, IPointerClickHandler
{
    private const float IDLE_ROTATE_AMOUNT = 2f; 
    private const float IDLE_ANIMATION_TIME = 2f;
    private const float ROTATE_TIME = .25f; 

    [Header("Text")]
    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private TMP_Text manaText;

    [Header("Panel")]
    [SerializeField]
    private RectTransform cardPanelTransform;

    public Card Card { get; private set; }

    private PassiveController passive = null;

    private Tween activeFlipAnimation = null;

    private Tween idleSequence = null;

    private CardDraggableSocket currentSocket = null;

    private bool frontShowing = true;

    public void Setup(Card card)
    {
        Card = card;
        titleText.text = card.CardName;
        descriptionText.text = card.Active.GetAbilityDescription();

        BeginAnimation();
    }
    
    void Update()
    {
        CheckForSocket();

        manaText.text = ManaGer.Instance.AdjustedCost(Card.BaseCost).ToString();
        DragEnabled = frontShowing && ManaGer.Instance.ManaAvailable(Card.BaseCost);
    }
    
    void OnDestroy()
    {
        if(idleSequence == null) {
            return;
        }

        idleSequence.Kill();
    }

    #region UI_ACTIONS

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

        if(PassiveActive() || !ManaGer.Instance.ManaAvailable(Card.BaseCost)) {
            return;
        }

        AbilityProcessor abilityProcessor = Card.Active.GetAbilityProcessor(characterManager);
        abilityProcessor.Process();
        DiscardCard();
        ManaGer.Instance.SpendMana(Card.BaseCost);
    }

    public void TogglePassive() {

        if(!ManaGer.Instance.ManaAvailable(Card.BaseCost)) {
            return;
        }

        if(!PassiveActive()) {
            ManaGer.Instance.SpendMana(Card.BaseCost);
            passive = Card.Passive.GetController();
            passive.Activate();
            Shrink();
        }
        else {
            passive.Deactivate();
            DiscardCard();
        }
    }

    public void DiscardCard()
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
        idleSequence = DOTween.Sequence()
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

    #endregion

    #region DRAGGABLE

    private void CheckForSocket()
    {
        if(!IsDragging) {
            return;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach(RaycastResult raycastResult in raycastResults) {
            CardDraggableSocket socket = raycastResult.gameObject.GetComponent<CardDraggableSocket>();
            if(socket != null) {
                ProcessSocketEntered(socket, raycastResult.screenPosition);
                return;
            }
        }

        if(currentSocket != null) {
            ProcessSocketExited();
        }
    }

    protected override void BeginDrag()
    {
        EnableSockets();
        base.BeginDrag();
    }

    protected override void EndDrag()
    {
        DisableSockets();

        if(currentSocket != null) {
            currentSocket.ProcessCardInserted(this);
            return;
        }
    
        base.EndDrag();
    }

    private void ProcessSocketExited() {
        currentSocket = null;
        Grow();
    }

    private void ProcessSocketEntered(CardDraggableSocket socket, Vector2 screenPosition) {

        if(socket == currentSocket) {
            return;
        }

        currentSocket = socket;
        transform.position = screenPosition;
        Shrink();
    }

    private void OnDisable()
    {   
        DisableSockets();
        base.EndDrag();
    }

    private void EnableSockets() {
        foreach(CardDraggableSocket socket in FindObjectsByType<CardDraggableSocket>(FindObjectsSortMode.None)) {
            socket.Show();
        }
    }

    private void DisableSockets() {
        foreach(CardDraggableSocket socket in FindObjectsByType<CardDraggableSocket>(FindObjectsSortMode.None)) {
            socket.Hide();
        }
    }

    #endregion
}