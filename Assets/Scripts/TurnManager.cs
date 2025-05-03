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
    public int totalPlayers = 0;
    public GameObject pawnPrefab;
    


    public CardDisplay cardDisplay;


    public int currentPlayer = 0;
    public BaseCard currentCard;

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
   
    public void AddPlayer()
    {
        players.Add(new Player(colors[totalPlayers]));

        totalPlayers++;
    }
    public void StartGame()
    {
        OnNextTurn.AddListener(NextTurn);
        Physics2D.queriesHitTriggers = true;


        foreach (var tile in startTiles)
        {
            getStartTile.Add(tile.color, tile);
        }

        foreach (var player in players)
        {
            for (int i = 0; i < piecesPerPlayer; i++)
            {
                var piece = Instantiate(pawnPrefab);
                var pawn = piece.GetComponent<Pawn>();

                pawn.color = player.color;
                pawn.GetComponent<SpriteRenderer>().color = actualColors[colors.IndexOf(pawn.color)];
                Debug.Log(getStartTile.Count);
                getStartTile[player.color].ApplyEffect(pawn);

                player.pieces.Add(pawn);
            }
        }
        //You would think we have to initialize the deck but since it was system serialized we dont have to since its initialized at compile time

        //This is just for now since when networking comes in i'll have to sync everyones deck (OR EVERYONE CAN REQUEST THE SERVER FOR A CARD)

    }
    public void DrawCard()
    {
        currentCard = deck.drawCard();
        cardDisplay.UpdateText();
        DrawButton.SetActive(false);
        

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
        

    }

    public void DisplayCard()
    {
        StartCoroutine(waitUntil());
    }
    private IEnumerator waitUntil()
    {
        cardDisplay.gameObject.SetActive(true);
        while (cardDisplay.gameObject.activeSelf)
        {

            yield return null;
        }

        if (!selectablePawns(currentCard.neededPawns[0]))
        {
            PlayerSync.Singleton.AllNextTurnRpc();

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
        Debug.Log("im bEING CLICKED HELP");
        PlayerSync.Singleton.SelectPieceRpc(whoPiece(selectedPawn));
        deselectPawns();  
        
    }
    public Player getCurrentPlayer()
    {
        return players[currentPlayer];
    }

    private int[] whoPiece(Pawn pawn)
    {
        int[] info = new int[2] {-1,-1}; //-1 just incase it somehow doesn't find it which should be impossible
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players[i].pieces.Count; j++)
            {
               if (players[i].pieces[j] == pawn)
                {
                    info[0] = i;
                    info[1] = j; 
                    break;
                }
            }
        }
        return info;
    }
}
