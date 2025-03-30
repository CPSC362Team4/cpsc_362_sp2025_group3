using UnityEngine;

public class ProtectedTile : StandardTile
{
    public override bool ApplyEffect(Pawn piece)
    {
        
        bool success = base.ApplyEffect(piece);
        if (success)
        {
            piece.state = PieceState.Protected;
        }
        return success;
    }
}
