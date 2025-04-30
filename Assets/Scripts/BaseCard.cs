using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : ScriptableObject //Scriptable Objects allow us to mass produce these
{
    
    public bool CanMoveSpawn; //whether or not the card allows you to move a piece from spawn
    public bool GoAgain;
    [TextArea(15, 20)]
    public string cardDescription; // in sorry when you draw a card it displays a description of what it does so thats what this is
    public Sprite cardImage;
    public PawnInfo[] neededPawns;

    public abstract bool CardEffect(List<Pawn> pieceToEffect); //Some cards move you forward while others let you split/swap so thats what this is

    
}

[System.Serializable]
public class PawnInfo
{
    public bool myPawns;
    public PieceState[] state;

}

