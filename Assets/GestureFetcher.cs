using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GestureFetcher : MonoBehaviour
{
    public Text playerText;
    public Text computerText;
    public Text resultText;

    public void PlayRound()
    {
        StartCoroutine(FetchGesture());
        Debug.Log("PlayRound triggered");
        StartCoroutine(FetchGesture());

    }

IEnumerator FetchGesture()
    {
        string url = "http://localhost:5000/gesture";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            playerText.text = "Error";
            yield break;
        }

        string json = request.downloadHandler.text;
        string playerGesture = JsonUtility.FromJson<GestureData>(json).gesture;
        string[] options = { "rock", "paper", "scissors" };
        string computerGesture = options[Random.Range(0, 3)];

        playerText.text = "You: " + playerGesture;
        computerText.text = "Computer: " + computerGesture;

        if (playerGesture == computerGesture)
            resultText.text = "Result: Tie!";
        else if ((playerGesture == "rock" && computerGesture == "scissors") ||
                 (playerGesture == "paper" && computerGesture == "rock") ||
                 (playerGesture == "scissors" && computerGesture == "paper"))
            resultText.text = "Result: You Win!";
        else
            resultText.text = "Result: You Lose!";
    }

    [System.Serializable]
    public class GestureData
    {
        public string gesture;
    }
}
