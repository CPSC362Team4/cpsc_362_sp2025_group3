using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public CardDeck deck;
    public List<Player> players = new List<Player>();


    public List<BaseTile> startTiles = new List<BaseTile>();
    public Dictionary<PieceColor, BaseTile> getStartTile = new Dictionary<PieceColor, BaseTile>();



    public int piecesPerPlayer; //not necessary but i thought might be cool
    public GameObject pawnPrefab;
    


    public CardDisplay cardDisplay;


    public int currentPlayer = 0;
    private BaseCard currentCard;

    public static TurnManager Singleton; //singleton pattern bc i really can't/don't want to think of way for other things to perform their actions

    private List<PieceColor> colors = new List<PieceColor>() { PieceColor.Yellow, PieceColor.Green, PieceColor.Red, PieceColor.Blue};

    public void Start()
    {
        Physics2D.queriesHitTriggers = true;
        if(Singleton == null) { Singleton = this; }

        foreach(var tile in startTiles)
        {
            getStartTile.Add(tile.color, tile);
        }


        players.Add(new Player(colors[0]));


        foreach(var player in players)
        {
            for (int i = 0; i < piecesPerPlayer; i++)
            {
                var piece = Instantiate(pawnPrefab);
                var pawn = piece.GetComponent<Pawn>();

                
                pawn.color = player.color;
                getStartTile[player.color].ApplyEffect(pawn);
  
                player.pieces.Add(pawn);
            }
        }
        //You would think we have to initialize the deck but since it was system serialized we dont have to since its initialized at compile time

        deck.fillDeck();//This is just for now since when networking comes in i'll have to sync everyones deck (OR EVERYONE CAN REQUEST THE SERVER FOR A CARD)
        
    }


    public void DrawCard()
    {
        currentCard = deck.drawCard();
        cardDisplay.UpdateText(currentCard.cardDescription, currentCard.cardImage);
        cardDisplay.gameObject.SetActive(true);
        StartCoroutine(waitUntil());

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

        selectablePawns(new List<Player>() { players[currentPlayer] });
    }

    private void selectablePawns(List<Player> makeSelectable)
    {
        Debug.Log("Im selectable1");
        foreach (var player in makeSelectable)
        {
            Debug.Log("Im selectable2");
            foreach (var piece in player.pieces)
            {
                Debug.Log("Im selectable3");
                piece.selectable = true;
            }
        }

    }
    private void deselectPawns(List<Player> makeDeselectable)
    {
        foreach (var player in makeDeselectable)
        {
            foreach (var piece in player.pieces)
            {
                piece.selectable = false;
            }
        }
    }
    public void SelectedPiece(Pawn selectedPawn)
    {

        currentCard.CardEffect(selectedPawn);
        deselectPawns(new List<Player>() { players[currentPlayer] });


    }
    public Player getCurrentPlayer()
    {
        return players[currentPlayer];
    }
}
