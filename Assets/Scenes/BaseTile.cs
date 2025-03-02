using System.Collections.Generic;
using UnityEngine;

// abstract base class for all tiles
public abstract class BaseTile : MonoBehaviour
{
    public enum TileColor { Red, Blue, Green, Yellow, None }  // here I can define tile color
    public TileColor color;  // this Stores tile color

    protected List<GameObject> piecesOnTile = new List<GameObject>();  // stores pieces on this tile

    // I will usde this method to add a piece to the tile
    public void AddPiece(GameObject piece)
    {
        piecesOnTile.Add(piece);
    }

    // this is a method to remove a piece from the tile
    public void RemovePiece(GameObject piece)
    {
        piecesOnTile.Remove(piece);
    }

    // this is a method to get all pieces currently on the tile
    public List<GameObject> GetPieces()
    {
        return piecesOnTile;
    }

    // Abstract method for tile effects (to be implemented in derived tile types)
    public abstract void ApplyEffect();
}
