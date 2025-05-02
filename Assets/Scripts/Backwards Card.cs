using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardSystem/Backwards Card")]
public class BackwardsCard : BaseCard
{
    public int value;
    public override bool CardEffect(List<Pawn> pieceToEffect)
    {
        BaseTile startTile = pieceToEffect[0].currentTile;
        BaseTile finalTile = startTile;
        for (int i = 0; i < value; i++)
        {
            
            finalTile = pieceToEffect[0].currentTile.prevTile;

            if (!finalTile.ApplyEffect(pieceToEffect[0])) //basically means move was illegal
            {

                startTile.ApplyEffect(pieceToEffect[0]);
                return false;
            }


        }

        finalTile.LandedOnEffect(pieceToEffect[0]);
        return true;
    }
}
