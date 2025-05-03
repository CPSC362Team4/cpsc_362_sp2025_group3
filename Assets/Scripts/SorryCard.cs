using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardSystem/SorryCard")]
public class SorryCard : BaseCard
{
    public override bool CardEffect(List<Pawn> pieceToEffect)
    {
        pieceToEffect[1].currentTile.ApplyEffect(pieceToEffect[0]);
        pieceToEffect[0].currentTile.LandedOnEffect(pieceToEffect[0]);
        return true;
    }

    
}
