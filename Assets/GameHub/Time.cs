using UnityEngine;
using TMPro;

public class ShowDateTime : MonoBehaviour
{
    public TextMeshProUGUI dateTimeText;

    void Update()
    {
        dateTimeText.text = System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
    }
}
