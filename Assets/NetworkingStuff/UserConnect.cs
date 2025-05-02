using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Services.Multiplayer;
using Unity.Multiplayer.Center;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;

public class UserConnect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Button relayButton;
    [SerializeField] private TMP_Text joinCodeText;
    [SerializeField] private TMP_InputField joinCodeInput;

    private async void Start()
    {
        joinCodeInput.onEndEdit.AddListener(JoinRelay);
        relayButton.onClick.AddListener(CreateRelay);



        await UnityServices.InitializeAsync();


        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //Anonymous sign in bc its easier could change this later to require actual login


    }

    public async void CreateRelay()
    {
        try
        {
            Debug.Log("Im creating host");
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            joinCodeText.text = joinCode;


            RelayServerData relayServerData = AllocationUtils.ToRelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);

        }

    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("Im joining host");
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);


            RelayServerData relayServerData = AllocationUtils.ToRelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
            //editor.SetActive(true);
            joinCodeInput.gameObject.SetActive(false);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);

        }

    }
}
