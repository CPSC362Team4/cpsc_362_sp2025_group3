using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Card", menuName = "CardSystem/BasicCard")]
public class Card : BaseCard
{
    public int value;
    

    public override void CardEffect(List<Pawn> pieceToEffect)
    {
        BaseTile startTile = pieceToEffect[0].currentTile;
        BaseTile finalTile = startTile;
        for (int i = 0; i < value; i++) 
        {
            Debug.Log(" i tried to move");
            finalTile = pieceToEffect[0].currentTile.nextTile;
            finalTile.ApplyEffect(pieceToEffect[0]);
            
        }
        finalTile.LandedOnEffect(pieceToEffect[0]);
    }
}
