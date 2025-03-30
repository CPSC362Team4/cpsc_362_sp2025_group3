using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public CardDeck deck;
    public List<Player> players = new List<Player>();


    public List<BaseTile> startTiles = new List<BaseTile>();
    public Dictionary<PieceColor, BaseTile> getStartTile = new Dictionary<PieceColor, BaseTile>();


    
    public CardDisplay cardDisplay;


    private int currentPlayer = 0;
    private BaseCard currentCard;

    public static TurnManager Singleton; //singleton pattern bc i really can't/don't want to think of way for other things to perform their actions



    public void Start()
    {
        if(Singleton == null) { Singleton = this; }

        foreach(var tile in startTiles)
        {
            getStartTile.Add(tile.color, tile);
        }


        players.Add(new Player());
        //You would think we have to initialize the deck but since it was system serialized we dont have to since its initialized at compile time

        deck.fillDeck();//This is just for now since when networking comes in i'll have to sync everyones deck (OR EVERYONE CAN REQUEST THE SERVER FOR A CARD)
        
    }


    public void DrawCard()
    {
        currentCard = deck.drawCard();
        cardDisplay.UpdateText(currentCard.cardDescription, currentCard.cardImage);
        cardDisplay.gameObject.SetActive(true);

    }


    
    public void NextTurn()
    {
        if (currentPlayer + 1 >= players.Count)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }

    }

    private IEnumerator waitUntil()
    {
        while (cardDisplay.gameObject.activeSelf)
        {
            yield return null;
        }
        
    }
}
