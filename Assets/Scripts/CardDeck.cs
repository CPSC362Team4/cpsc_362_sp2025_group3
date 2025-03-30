using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardDeck //since turn manager will be our mono behavior no need to also make this one since that means we'll have to have it in the scene
{
    private List<BaseCard> deck = new List<BaseCard>();
    
    public CardDataBase CardData; //YOU CAN ASSIGN IN INSPECTOR IF ITS SERIALIZED THATS ACTUALLY CRAZY I DIDNT KNOW

    
    public BaseCard drawCard()
    {
        if(deck.Count <= 0) 
        {
            fillDeck();
        }

        BaseCard temp = deck[0];
        deck.RemoveAt(0);
        return temp;
    }
    
  /*  public void showDeck() Since it works a lot differently than this this is no longer needed since we'll be able to check directly
    {
        foreach(BaseCard card in deck)
            Debug.Log(card.cardDescription);
    }*/

    public void fillDeck()
    {
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(CardData);
            deck.Add(CardData.GetCards[0]); 
            //We just need to keep in mind that the first card of the database will always contain 5 (we could do another class that contains how much it should contain of each type but nah)
        }
        for(int i = 0; i < 4; i++) 
        {

            for (int j = 1; j < CardData.cards.Length; j++ ) //Since we'll be able to define the card database theres no longer a need to exclude 6 and 9
            {
                deck.Add(CardData.GetCards[j]);
            }
            
        
        }
        
        for(int i = 0; i < deck.Count; i++)
        {
            int swapIdx = Random.Range(i, deck.Count-1);
            BaseCard temp = deck[swapIdx];
            deck[swapIdx] = deck[i];
            deck[i] = temp;
        }

    }


    
}
