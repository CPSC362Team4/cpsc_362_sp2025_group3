using UnityEngine;

public class AltPathTile : StandardTile
{
    [Tooltip("Will go to this tile instead if Alt condition is met")]
    public BaseTile AltTile; //sometimes tiles have an alternate path that they follow like going home instead of looping or sliding but they don't always do so

    [Tooltip("True = Alt path will be taken if same color \nFalse=Alt path will be taken if different color")]
    public bool ColorOrNot;
    public override bool ApplyEffect(Pawn piece)
    {
        bool success;
        if (!(ColorOrNot ^ piece.color == color)) //the forbidden xand/xnor gate
        {
            success = AltTile.ApplyEffect(piece);
        }
        else
        {
            success = base.ApplyEffect(piece);
        }
        
        
        return success;
    }


}
