using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GridLayoutGroup cardsHolder;
    private List<Card> cards;

    private Card firstCardClick;

    private void Start()
    {
        cards = new List<Card>();
        var card = Instantiate(cardPrefab, cardsHolder.transform).GetComponent<Card>();
        card.AddOnClick(OnCardClicked);
        card.SetCard(1, 1, null);
        cards.Add(card);
        card = Instantiate(cardPrefab, cardsHolder.transform).GetComponent<Card>();
        card.AddOnClick(OnCardClicked);
        card.SetCard(1, 1, null);
        cards.Add(card);
    }
    private void OnCardClicked(Card card)
    {
        if(firstCardClick == null)
        {
            firstCardClick = card;
            firstCardClick.AnimateFlipping();
        }
        else
        {
            card.AnimateFlipping();
            if(firstCardClick.DoesPair(card))
            {
                ///Ganhar card
            }
            else
            {

                firstCardClick = null;
            }
        }
        Debug.Log("Card clicked " + card.GetCardId());
    }
}
