using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<Pawn> pieces = new List<Pawn>();

    public PieceColor color;

    public Player(PieceColor color)
    {
        this.color = color;
    }
}
