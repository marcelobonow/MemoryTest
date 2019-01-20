using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardState
{
    normal,
    flipping,
    flipped,
    unflipping
}

public class Card : MonoBehaviour
{
    [SerializeField] private Animator cardAnimator;
    [SerializeField] private Image frontCardImage;
    [SerializeField] private Image backCardImage;

    private int spriteId;
    private int cardId;
    private CardState currentState;

    private Action OnCardUnflip;
    private Action OnCardFlip;
    private Action<Card> OnCardClicked;

    private void Awake()
    {
        currentState = CardState.normal;
    }
    public void SetCard(int spriteId, int cardId, Sprite sprite)
    {
        frontCardImage.sprite = sprite;
        this.spriteId = spriteId;
        this.cardId = cardId;
    }
    public int GetCardId()
    {
        return cardId;
    }

    public void AddOnCardUnflip(Action action)
    {
        OnCardUnflip += action;
    }
    public void AddOnCardFlip(Action action)
    {
        OnCardFlip += action;
    }
    public void AddOnClick(Action<Card> action)
    {
        OnCardClicked += action;
    }

    public bool DoesPair(Card otherCard)
    {
        return otherCard.GetCardId() == cardId;
    }
    public void OnClick()
    {
        if(OnCardClicked != null)
            OnCardClicked(this);
    }

    public void AnimateFlipping()
    {
        cardAnimator.SetTrigger("Flip");
        currentState = CardState.flipping;
    }
    public void AnimateUnflipping()
    {
        cardAnimator.SetTrigger("Unflip");
        currentState = CardState.unflipping;
    }

    public void OnEndFlip()
    {
        currentState = CardState.flipped;
        if(OnCardFlip != null)
        {
            OnCardFlip();
            OnCardFlip = null;
        }
    }
    public void OnEndUnflip()
    {
        currentState = CardState.normal;
        if(OnCardUnflip != null)
        {
            OnCardUnflip();
            OnCardUnflip = null;
        }
    }
}
