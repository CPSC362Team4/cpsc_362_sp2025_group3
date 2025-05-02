using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.Events;

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

    public List<PieceColor> colors = new List<PieceColor>() { PieceColor.Yellow, PieceColor.Green, PieceColor.Red, PieceColor.Blue};
    public List<Color> actualColors = new List<Color>();

    public List<Pawn> currentlySelectedPawns = new List<Pawn>(); //this just makes the most sense since sometimes more than one pawn would be required
    public GameObject DrawButton;

    public UnityEvent OnNextTurn;

    private void Awake()
    {
        OnNextTurn = new UnityEvent();
        if (Singleton == null) { Singleton = this; }
    }
    public void Start()
    {
        
        Physics2D.queriesHitTriggers = true;
        

        foreach(var tile in startTiles)
        {
            getStartTile.Add(tile.color, tile);
        }


        players.Add(new Player(colors[0]));
        players.Add(new Player(colors[1]));

        foreach(var player in players)
        {
            for (int i = 0; i < piecesPerPlayer; i++)
            {
                var piece = Instantiate(pawnPrefab);
                var pawn = piece.GetComponent<Pawn>();

                pawn.color = player.color;
                pawn.GetComponent<SpriteRenderer>().color = actualColors[colors.IndexOf(pawn.color)];
                
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
        DrawButton.SetActive(false);
        cardDisplay.gameObject.SetActive(true);
        StartCoroutine(waitUntil());

    }
    

    
    public void NextTurn()
    {
        if (!currentCard.GoAgain)
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
        DrawButton.SetActive(true);
        OnNextTurn.Invoke();

    }

    private IEnumerator waitUntil()
    {
        while (cardDisplay.gameObject.activeSelf)
        {

            yield return null;
        }

        if (!selectablePawns(currentCard.neededPawns[0]))
        {
            NextTurn();
            
        }
    }

    public bool selectablePawns(PawnInfo makeSelectable)
    {
        bool something = false;
        if (makeSelectable.myPawns)//im gonna be so fr this gave me the big pain but this is the easiest way i can think of to allow specific selection of pawns
        {       
            foreach (var piece in getCurrentPlayer().pieces)
            {

                if (makeSelectable.state.Contains(piece.state) && currentCard.CanMove(piece))
                {

                    piece.selectable = true;
                    something = true;
                }

            }

            return something;         
        }

        
        foreach (var player in players)
        {
            
            if(player == getCurrentPlayer()) { continue; }

            foreach (var piece in player.pieces)
            {

                if (makeSelectable.state.Contains(piece.state))
                {
                    piece.selectable = true;
                    something = true;
                }
            }
        }
        return something;
    }
    private void deselectPawns()
    {
        //for sure a way to optimize this instead of a 2d loop but not the main concern rn
        foreach (var player in players)
        {
            foreach (var piece in player.pieces)
            {
                piece.selectable = false;
            }
        }
    }
    public void SelectedPiece(Pawn selectedPawn)
    {
        currentlySelectedPawns.Add(selectedPawn);
        deselectPawns();
        if (currentlySelectedPawns.Count >= currentCard.neededPawns.Length)
        {
            if(currentCard.CardEffect(currentlySelectedPawns) )
            {
                NextTurn();
            }
            //probably go next turn if successful (?)
            currentlySelectedPawns.Clear();
            
        }
        else
        {
            selectablePawns(currentCard.neededPawns[currentlySelectedPawns.Count]);
        }
        
    }
    public Player getCurrentPlayer()
    {
        return players[currentPlayer];
    }
}
