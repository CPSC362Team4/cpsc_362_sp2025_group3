using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Card", menuName = "CardSystem/BasicCard")]
public class Card : BaseCard
{
    public int value;
    

    public override void CardEffect(Pawn pieceToEffect)
    {
        
    }
}
