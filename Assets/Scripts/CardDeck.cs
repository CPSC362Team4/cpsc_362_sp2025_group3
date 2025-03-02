using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    List<Card> deck = new List<Card>();

    public void drawCard()
    {
        Card temp = deck[0];
        deck.Remove(temp);
        Debug.Log(temp.value);
    }
    
    public void showDeck()
    {
        foreach(Card card in deck)
            Debug.Log(card.value);
    }

    public void fillDeck()
    {
        for(int i = 0; i < 5; i++)
        {
            deck.Add(new Card(1));
        }
        for(int i = 0; i < 4; i++)
        {
            deck.Add(new Card(2));
            deck.Add(new Card(3));
            deck.Add(new Card(4));
            deck.Add(new Card(5));
            deck.Add(new Card(7));
            deck.Add(new Card(8));
            deck.Add(new Card(10));
            deck.Add(new Card(11));
            deck.Add(new Card(12));
            deck.Add(new Card(13));
        }
        
        for(int i = 0; i < deck.Count; i++)
        {
            int swapIdx = Random.Range(i, deck.Count-1);
            Card temp = deck[swapIdx];
            deck[swapIdx] = deck[i];
            deck[i] = temp;
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillDeck();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
