using UnityEngine;

public class SlideTile : StandardTile
{
    public BaseTile targetTile;

    public override void LandedOnEffect(Pawn piece)
    {
        if (piece.color != color)
        {
            targetTile.ApplyEffect(piece);
        }
    }
}
