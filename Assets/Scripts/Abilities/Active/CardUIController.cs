using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUIController : Draggable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    [SerializeField]
    private RectTransform cardBaseTransform;

    [Header("Material")]
    [SerializeField]
    private Image cardHeader;

    [SerializeField]
    private Image cardFooter;

    [SerializeField]
    private Color mageColor = Color.blue;

    [SerializeField]
    private Color rogueColor = Color.red;

    [SerializeField]
    private Color tankColor = Color.green;

    [SerializeField]
    private Color allClass = Color.gray;

    [SerializeField]
    private Color activeTextColor = Color.white;

    [SerializeField]
    private Color passiveTextColor = Color.black;

    public Card Card { get; private set; }

    private PassiveController passive = null;

    private Tween activeFlipAnimation = null;

    private Tween idleSequence = null;

    private CardDraggableSocket currentSocket = null;

    private bool frontShowing = true;

    private int prevIndex = 0;

    private Material cardHeaderMat;

    private Material cardFooterMat;

    public void Setup(Card card)
    {
        Card = card;
        titleText.text = card.CardName;
        descriptionText.text = card.Active.GetAbilityDescription();

        cardHeaderMat = Instantiate(cardHeader.material);
        cardHeader.material = cardHeaderMat;
        
        cardFooterMat = Instantiate(cardFooter.material);
        cardFooter.material = cardFooterMat;

        SetCardColor();
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
            .Append(cardBaseTransform.DOLocalRotate(Vector3.up * 90, ROTATE_TIME / 2).SetEase(Ease.InCubic).OnComplete(UpdateFlippingCard))
            .Append(cardBaseTransform.DOLocalRotate(Vector3.zero, ROTATE_TIME / 2).SetEase(Ease.OutCubic))
            .Play()
            .OnComplete(() => {
                activeFlipAnimation = null;
            });

    }

    private void UpdateFlippingCard() {
        cardBaseTransform.localEulerAngles = Vector3.up * -90f;
        descriptionText.text = frontShowing ? Card.Active.GetAbilityDescription() : Card.Passive.GetAbilityDescription();
        descriptionText.color = frontShowing ? activeTextColor : passiveTextColor;
        cardFooterMat.SetInt("_Passive", frontShowing ? 0 : 1);
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

        ActiveAbilityProcessor abilityProcessor = Card.Active.GetAbilityProcessor(characterManager);
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
        cardPanelTransform.DOScale(Vector3.one * .8f, ROTATE_TIME / 2).SetEase(Ease.InOutCubic);
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
            if(socket != null && socket.Insertable) {
                ProcessSocketEntered(socket);
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

    protected override void SetPosition(PointerEventData eventData)
    {
        if(!IsDragging) {
            return;
        }

        if(currentSocket != null) {
            draggableTransformOverride.position = new Vector3(currentSocket.transform.position.x, currentSocket.transform.position.y, 0);
            return;
        }

        base.SetPosition(eventData);
    }

    private void ProcessSocketExited() {
        currentSocket = null;
        Grow();
    }

    private void ProcessSocketEntered(CardDraggableSocket socket) {

        if(socket == currentSocket) {
            return;
        }

        currentSocket = socket;
        Shrink();
    }

    private void OnDisable()
    {   
        DisableSockets();
        base.EndDrag();
    }

    private void EnableSockets() {
        foreach(CardDraggableSocket socket in FindObjectsByType<CardDraggableSocket>(FindObjectsSortMode.None)) {
            if(socket.CanProcessCard(Card)) {
                socket.Show();
            }
        }
    }

    private void DisableSockets() {
        foreach(CardDraggableSocket socket in FindObjectsByType<CardDraggableSocket>(FindObjectsSortMode.None)) {
            socket.Hide();
        }
    }

    public void TweenToPos(Vector3 pos, float easeTime) {
        GetComponent<RectTransform>().DOAnchorPos(pos, duration: easeTime).SetEase(Ease.InOutCubic);
    }

    // Move to the front if hovering over the card
    public void OnPointerEnter(PointerEventData eventData)
    {
        prevIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.SetSiblingIndex(prevIndex);
    }

    #endregion

    #region Material

    private void SetCardColor() {

        Color color = allClass;

        if(Card.CharacterClasses.Count == 1) {
            PlayerClass playerClass = Card.CharacterClasses.First();
            color = playerClass switch {
                PlayerClass.MAGE => mageColor,
                PlayerClass.ROGUE => rogueColor,
                PlayerClass.TANK => tankColor,
                _ => allClass
            };
        }

        cardHeaderMat.SetColor("_CardColor", color);
    }

    #endregion
}