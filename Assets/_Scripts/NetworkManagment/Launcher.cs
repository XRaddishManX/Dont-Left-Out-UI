using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    public bool IsConnectedToLobby { get; private set; }

    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text errorMessage;
    [SerializeField] private TMP_InputField nickNameInput;
    
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomItemPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerItemPrefab;

    [SerializeField] private GameObject myPlayerManagerPrefab;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsConnectedToLobby = false;
        MenuManager.Instance.OpenMenuName("HomeScreen");
    }

    public void ConnectToPhotonServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.NickName.IsNullOrEmpty())
        {
            PhotonNetwork.NickName = "player" + Random.Range(0, 1000).ToString("0000");
        }
        if (!IsConnectedToLobby)
        {
            MenuManager.Instance.CloseMenuName("HomeScreen");
            IsConnectedToLobby = true;
        }
        Debug.Log("Connected to Lobby. You are " + PhotonNetwork.NickName);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenuName("Room");
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        
        PhotonNetwork.Instantiate(this.myPlayerManagerPrefab.name, 
            new Vector3(-200,10,0), Quaternion.identity);
       
        
        foreach(Transform playerT in playerListContent)
        {Destroy(playerT.gameObject);}

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i <players.Length; i++)
        {
            Instantiate(playerItemPrefab,
                playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMessage.text = "No se pudo crear la sala. Error: " + message;
        MenuManager.Instance.OpenMenuName("Error");
        
    }

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.CloseMenuName("Room");
        MenuManager.Instance.OpenMenuName("Play");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transformRoomList in roomListContent)
        {
            Destroy(transformRoomList.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomItemPrefab,roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public void ChangeNickName()
    {
        PhotonNetwork.NickName = nickNameInput.text;
        Debug.Log(PhotonNetwork.NickName);
    }
}
