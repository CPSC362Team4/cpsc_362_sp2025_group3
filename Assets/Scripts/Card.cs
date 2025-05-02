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
            
            finalTile = pieceToEffect[0].currentTile.nextTile;
            
            if (finalTile == null || !finalTile.ApplyEffect(pieceToEffect[0])) //basically means move was illegal
            {
                Debug.Log("move fail");
                startTile.ApplyEffect(pieceToEffect[0]);
                return false;
            }
            
            
        }

        Debug.Log("move success");
        finalTile.LandedOnEffect(pieceToEffect[0]);
        return true;
    }
    public override bool CanMove(Pawn pawn)
    {
        BaseTile startTile = pawn.currentTile;
        BaseTile finalTile = startTile;
        for (int i = 0; i < value; i++)
        {
           
            finalTile = pawn.currentTile.nextTile;

            if (finalTile == null || !finalTile.ApplyEffect(pawn)) //basically means move was illegal
            {

                startTile.ApplyEffect(pawn);
                return false;
            }


        }
        Debug.Log("I can move");
        startTile.ApplyEffect(pawn);
        return true;
    }
}
