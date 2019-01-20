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
    private CardsController cardController;
    private CardState currentState;

    private Action OnCardUnflip;
    private Action OnCardFlip;

    private void Awake()
    {
        currentState = CardState.normal;
    }
    public void SetCard(int spriteId, int cardId, Sprite sprite, CardsController controller)
    {
        frontCardImage.sprite = sprite;
        this.spriteId = spriteId;
        cardController = controller;
    }
    public int GetSpriteId()
    {
        return spriteId;
    }
    public void AddOnCardUnflip(Action action)
    {
        OnCardUnflip += action;
    }
    public void AddOnCardFlip(Action action)
    {
        OnCardFlip += action;
    }
    public bool DoesPair(Card otherCard)
    {
        return otherCard.GetSpriteId() == spriteId;
    }
    public void OnClick()
    {
        cardController.OnCardClick(this);
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

    public void EndFlip()
    {
        currentState = CardState.flipped;
        if(OnCardFlip != null)
        {
            OnCardFlip();
            OnCardFlip = null;
        }
    }
    public void EndUnflip()
    {
        currentState = CardState.normal;
        if(OnCardUnflip != null)
        {
            OnCardUnflip();
            OnCardUnflip = null;
        }
    }
}
