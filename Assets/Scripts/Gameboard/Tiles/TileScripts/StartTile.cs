using UnityEngine;

public class StartTile : BaseTile
{


    public override bool ApplyEffect(Pawn piece)
    {
        piece.state = PieceState.Start;
        return base.ApplyEffect(piece);
    }

    public override void LandedOnEffect(Pawn piece)
    {

    }
}
