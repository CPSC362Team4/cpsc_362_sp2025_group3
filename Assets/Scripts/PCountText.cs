using TMPro;
using UnityEngine;

public class PCountText : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        text.text = "Connected Players " + SceneManager.Singleton.totalPlayers;
    }
}
