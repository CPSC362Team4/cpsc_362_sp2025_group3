using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Card", menuName = "CardSystem/BasicCard")]
public class Card : BaseCard
{
    public int value;
    

    public override void CardEffect(Pawn pieceToEffect)
    {
        BaseTile startTile = pieceToEffect.currentTile;
        BaseTile finalTile = startTile;
        for (int i = 0; i < value; i++) 
        {
            Debug.Log(" i tried to move");
            finalTile = pieceToEffect.currentTile.nextTile;
            finalTile.ApplyEffect(pieceToEffect);
            
        }
        finalTile.LandedOnEffect(pieceToEffect);
    }
}
