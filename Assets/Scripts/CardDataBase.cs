using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card DataBase", menuName = "CardSystem/CardDataBase")]
public class CardDataBase : ScriptableObject, ISerializationCallbackReceiver //in case you're wondering why there's a seperate database its bc over the network we can/should only send basic data types and we can translate them locally
{
    public BaseCard[] cards;

    public Dictionary<BaseCard, int> GetId = new Dictionary<BaseCard, int>();
    public Dictionary<int, BaseCard> GetCards = new Dictionary<int, BaseCard>();

    //

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<BaseCard, int>();
        GetCards = new Dictionary<int, BaseCard>();
        for (int i = 0; i < cards.Length; i++)
        {
            GetId.Add(cards[i], i);
            GetCards.Add(i, cards[i]);
        }

    }

    public void OnBeforeSerialize()
    {

    }
}
