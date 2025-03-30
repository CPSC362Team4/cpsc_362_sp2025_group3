using UnityEngine;

public abstract class BaseCard : ScriptableObject //Scriptable Objects allow us to mass produce these
{
    [TextArea(15, 20)]

    public bool CanMoveSpawn; //whether or not the card allows you to move a piece from spawn
    public string cardDescription; // in sorry when you draw a card it displays a description of what it does so thats what this is
    public Sprite cardImage;

    public abstract void CardEffect(Pawn pieceToEffect); //Some cards move you forward while others let you split/swap so thats what this is
}
