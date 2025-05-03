using UnityEngine;

public class HomeTile : BaseTile
{
    

    public override bool ApplyEffect(Pawn piece)
    {
        piece.state = PieceState.Home;
        return base.ApplyEffect(piece);
    }
    public override void LandedOnEffect(Pawn piece)
    {
       
    }
}
