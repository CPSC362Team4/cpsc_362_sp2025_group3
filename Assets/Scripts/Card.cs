using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Card", menuName = "CardSystem/BasicCard")]
public class Card : BaseCard
{
    public int value;
    

    public override bool CardEffect(List<Pawn> pieceToEffect)
    {
        BaseTile startTile = pieceToEffect[0].currentTile;
        BaseTile finalTile = startTile;
        for (int i = 0; i < value; i++) 
        {
            Debug.Log(" i tried to move");
            finalTile = pieceToEffect[0].currentTile.nextTile;
            
            if (!finalTile.ApplyEffect(pieceToEffect[0])) //basically means move was illegal
            {

                startTile.ApplyEffect(pieceToEffect[0]);
                return false;
            }
            
            
        }
        
        finalTile.LandedOnEffect(pieceToEffect[0]);
        return true;
    }
    public override bool CanMove(Pawn pawn)
    {
        BaseTile startTile = pawn.currentTile;
        BaseTile finalTile = startTile;
        for (int i = 0; i < value; i++)
        {
            Debug.Log(" i tried to move");
            finalTile = pawn.currentTile.nextTile;

            if (!finalTile.ApplyEffect(pawn)) //basically means move was illegal
            {

                startTile.ApplyEffect(pawn);
                return false;
            }


        }
        startTile.ApplyEffect(pawn);
        return true;
    }
}
