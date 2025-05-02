using System.Collections.Generic;
using UnityEngine;

// abstract base class for all tiles
public abstract class BaseTile : MonoBehaviour
{

    public PieceColor color;  // this Stores tile color

    public BaseTile nextTile;
    public BaseTile prevTile;



    protected List<Pawn> piecesOnTile = new List<Pawn>();  // stores pieces on this tile (though the list functionality only matters for home tiles)
    
    //im just gonna keep this out since this and apply effect share the exact same function anyways
    /*public virtual void AddPiece(Pawn piece) 
    {
        piecesOnTile.Add(piece);
    }*/

    // this is a method to remove a piece from the tile
    public void RemovePiece()
    {
        piecesOnTile.RemoveAt(0);
    }

    // this is a method to get all pieces currently on the tile, note unecessary since in sorry this'll never be possible (your pieces goomba stomp others when they land on the same tile)
   /* public List<GameObject> GetPieces()
    {
        return piecesOnTile;
    }
*/

    // Virtual method for tile effects (to be implemented in derived tile types) 
    public virtual bool ApplyEffect(Pawn piece) //the reason for swap to bool is because when we move we have to know if its a legal move but for base class we'll assume so
    {
        if(piece.currentTile != null)
        {
            piece.currentTile.RemovePiece();
        }
        piece.currentTile = this;
        piecesOnTile.Add(piece);
        piece.gameObject.transform.position = gameObject.transform.position;
        return true;
    } 

    public virtual void LandedOnEffect(Pawn piece)
    {
       
    }
}
