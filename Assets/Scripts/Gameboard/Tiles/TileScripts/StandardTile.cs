using UnityEngine;

public class StandardTile : BaseTile
{


    public override bool ApplyEffect(Pawn piece)
    {

        if (piecesOnTile.Count > 0)
        {
            if (piecesOnTile[0].color == piece.color)
            {
                return false;
            }
            piecesOnTile[0].currentTile = TurnManager.Singleton.getStartTile[color];
            TurnManager.Singleton.getStartTile[color].ApplyEffect(piecesOnTile[0]);
            piecesOnTile.RemoveAt(0);

        }
        piece.state = PieceState.Active;
        return base.ApplyEffect(piece);
    }

}
