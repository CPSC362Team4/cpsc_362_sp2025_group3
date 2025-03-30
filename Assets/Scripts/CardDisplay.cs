using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public TextMeshProUGUI cardText;
    public Image[] cardNumber;
    

    public void UpdateText(string text, Sprite cardImage)
    {
        cardText.text = text;

        foreach(var card in cardNumber)
        {
            card.sprite = cardImage;
        }
        
    }
}
