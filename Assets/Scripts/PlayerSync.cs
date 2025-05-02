using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerSync : NetworkBehaviour
{
    public static PlayerSync Singleton;


    public void Awake()
    {
        if (Singleton != null) { return; }

        Singleton = this;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        if (IsHost)
        {
            SpawnPlayersRpc(SceneManager.Singleton.totalPlayers);
        }
    }
    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false)]
    private void SpawnPlayersRpc(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            TurnManager.Singleton.AddPlayer();
        }
        TurnManager.Singleton.StartGame();
    }
    public void DrawCard()
    {
        
        if (IsValid(TurnManager.Singleton.currentPlayer, NetworkManager.Singleton.LocalClientId) )
        {
            DrawCardRpc();
        }
    }
    [Rpc(SendTo.Server, RequireOwnership = false)]
    public void DrawCardRpc(RpcParams param = default)
    {
        
        if (IsValid(TurnManager.Singleton.currentPlayer, param.Receive.SenderClientId))
        {
            var toolong = TurnManager.Singleton.deck.CardData;
            SyncCardRpc(UnityEngine.Random.Range(0, toolong.cards.Length));
        }
    }
    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false)]
    public void SyncCardRpc(int cardId)
    {
        TurnManager.Singleton.deck.AddCard(cardId);
        TurnManager.Singleton.DrawCard();
        if (IsValid(TurnManager.Singleton.currentPlayer, NetworkManager.Singleton.LocalClientId))
        {
            
            TurnManager.Singleton.cardDisplay.UpdateText();
            TurnManager.Singleton.DisplayCard();
        }
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false)]
    public void AllNextTurnRpc()
    {
        TurnManager.Singleton.OnNextTurn.Invoke();
    }
    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false)]
    public void SelectPieceRpc(int[] pieceInfo)
    {
        var cache = TurnManager.Singleton;
        cache.currentlySelectedPawns.Add(cache.players[pieceInfo[0]].pieces[pieceInfo[1]]);
        if (cache.currentlySelectedPawns.Count >= cache.currentCard.neededPawns.Length)
        {
            Debug.Log("Im selected " + cache.currentlySelectedPawns[0].name);
            cache.currentCard.CardEffect(cache.currentlySelectedPawns);

            cache.currentlySelectedPawns.Clear();

            TurnManager.Singleton.OnNextTurn.Invoke();
             //getting pretty messy but i gotta get this done
        }
        else if (IsValid(TurnManager.Singleton.currentPlayer, NetworkManager.Singleton.LocalClientId))
        {
            cache.selectablePawns(cache.currentCard.neededPawns[cache.currentlySelectedPawns.Count]);
        }
    }

    private bool IsValid(int id, ulong id2)
    {
        return id == Convert.ToInt32(id2);
    }
}
