using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomListName;
    public RoomInfo RoomInfo;

    public void SetUp(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        roomListName.text = roomInfo.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(RoomInfo);
    }
}
