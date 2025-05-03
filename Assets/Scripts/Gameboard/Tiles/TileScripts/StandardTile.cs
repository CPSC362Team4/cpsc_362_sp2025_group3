using UnityEngine;

public class StandardTile : BaseTile
{


    public override bool ApplyEffect(Pawn piece)
    {

        
        piece.state = PieceState.Active;
        return base.ApplyEffect(piece);
    }
    
}
