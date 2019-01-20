using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GridLayoutGroup cardsHolder;
    private List<Card> cards;

    private void Start()
    {
        cards = new List<Card>();
        var card = Instantiate(cardPrefab, cardsHolder.transform).GetComponent<Card>();
        card.AddOnClick(OnCardClicked);
        cards.Add(card);
    }
    private void OnCardClicked(Card card)
    {
        Debug.Log("Card clicked " + card.GetCardId());
    }
}
