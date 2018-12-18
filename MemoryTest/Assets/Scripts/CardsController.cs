using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{
    [SerializeField] private float timeBetweenFlip;
    [SerializeField] private Sprite[] cardsSprite;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform cardsHolder;

    private List<Card> cards;

    private Card firstCardSelected;
    private Card secondCardSelected;

    private void Awake()
    {
        cards = new List<Card>();
        var cardsObject = GameObject.FindGameObjectsWithTag("cards");
        var cardsQuantity = cardsObject.Length;
        foreach(var cardObject in cardsObject)
        {
            cards.Add(cardObject.GetComponent<Card>());
        }
        var cardsValues = GetRandomValues(cardsQuantity);
        for(var i = 0; i < cardsQuantity; i++)
        {
            var cardComponent = cards[i];
            cardComponent.SetCard(cardsValues[i], i, cardsSprite[cardsValues[i]], this);
            cards.Add(cardComponent);
        }
    }
    private List<int> GetRandomValues(int totalCards)
    {
        var allNumbers = new List<int>();
        var selectedNumbers = new List<int>();
        for(var i = 0; i < cardsSprite.Length; i++)
        {
            allNumbers.Add(i);
        }
        for(var i = 0; i < totalCards / 2; i++)
        {
            var randomNumber = Random.Range(0, allNumbers.Count);
            ///Adiciona um par
            selectedNumbers.Add(allNumbers[randomNumber]);
            selectedNumbers.Add(allNumbers[randomNumber]);
            allNumbers.RemoveAt(randomNumber);
        }
        selectedNumbers.Shuffle();
        return selectedNumbers;
    }

    public void OnCardClick(Card card)
    {
        if(firstCardSelected == null)
        {
            firstCardSelected = card;
            card.AnimateFlipping();
        }
        else if(secondCardSelected == null)
        {
            secondCardSelected = card;
            card.AddOnCardUnflip(ResetSelectedCards);
            card.AddOnCardFlip(OnCardsFlipped);
            card.AnimateFlipping();
        }
    }
    private void OnCardsFlipped()
    {
        if(!firstCardSelected.DoesPair(secondCardSelected))
        {
            StartCoroutine(UnflipCards());
        }
        else
        {
            ResetSelectedCards();
        }
    }

    private IEnumerator UnflipCards()
    {
        yield return new WaitForSeconds(timeBetweenFlip);
        firstCardSelected.AnimateUnflipping();
        secondCardSelected.AnimateUnflipping();
    }
    private void ResetSelectedCards()
    {
        firstCardSelected = null;
        secondCardSelected = null;
    }
}
