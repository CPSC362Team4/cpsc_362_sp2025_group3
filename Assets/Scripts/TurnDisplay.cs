using TMPro;
using UnityEngine;

public class TurnDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerTurnText;
    void Start()
    {
        
        TurnManager.Singleton.OnNextTurn.AddListener(UpdateDisplay);  
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        playerTurnText.color = TurnManager.Singleton.actualColors[TurnManager.Singleton.currentPlayer];
        playerTurnText.text = "Player " + (TurnManager.Singleton.currentPlayer + 1);
    }
}
