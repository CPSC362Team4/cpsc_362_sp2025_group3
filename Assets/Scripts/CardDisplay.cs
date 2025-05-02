using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public TextMeshProUGUI cardText;
    public Image[] cardNumber;
    

    public void UpdateText()
    {
        cardText.text = TurnManager.Singleton.currentCard.cardDescription;

        foreach(var card in cardNumber)
        {
            card.sprite = TurnManager.Singleton.currentCard.cardImage;
        }
        
    }
}
