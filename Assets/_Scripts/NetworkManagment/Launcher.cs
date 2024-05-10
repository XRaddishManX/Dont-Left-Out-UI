using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using WebSocketSharp;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] public bool isAlreadyInMainMenu;

    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text errorMessage;
    [SerializeField] private TMP_InputField nickNameInput;
    
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomItemPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerItemPrefab;

    [SerializeField] private GameObject myPlayerManagerPrefab;

    private Dictionary<string, RoomInfo> _roomList;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isAlreadyInMainMenu = false;
        MenuManager.Instance.OpenMenuName("HomeScreen");
        _roomList = new Dictionary<string, RoomInfo>();
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
        if (!isAlreadyInMainMenu)
        {
            PhotonNetwork.NickName = "player" + Random.Range(0, 1000).ToString("0000");
            MenuManager.Instance.CloseMenuName("HomeScreen");
            isAlreadyInMainMenu = true;
        }
        Debug.Log("Connected to Lobby. You are " + PhotonNetwork.NickName);
        _roomList.Clear();
    }

    public override void OnLeftLobby()
    {
        _roomList.Clear();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _roomList.Clear();
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
        ClearRoomList();
        UpdateRoomList(roomList);
        UpdateRoomListView();
    }

    private void UpdateRoomListView()
    {
        foreach (RoomInfo roomInfo in _roomList.Values)
        {
            Instantiate(roomItemPrefab,roomListContent).GetComponent<RoomListItem>().SetUp(roomInfo);
        }
    }

    private void UpdateRoomList(List<RoomInfo> roomInfos)
    {
        foreach (RoomInfo roomInfo in roomInfos)
        {
            if (!roomInfo.IsOpen || !roomInfo.IsVisible || roomInfo.RemovedFromList)
            {
                if (_roomList.ContainsKey(roomInfo.Name))
                {
                    _roomList.Remove(roomInfo.Name);
                }
                continue;
            }
            
            if (_roomList.ContainsKey(roomInfo.Name))
            {
                _roomList[roomInfo.Name] = roomInfo;
            }
            else
            {
                _roomList.Add(roomInfo.Name, roomInfo);
            }
        }
        
    }

    private void ClearRoomList()
    {
        foreach (Transform transformRoomList in roomListContent)
        {
            Destroy(transformRoomList.gameObject);
        }
    }

    public void ChangeNickName()
    {
        if (string.IsNullOrEmpty(nickNameInput.text))
        {
            PhotonNetwork.NickName = nickNameInput.text;
        }
        Debug.Log(PhotonNetwork.NickName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
