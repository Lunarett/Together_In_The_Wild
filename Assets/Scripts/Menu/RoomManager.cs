using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
	[SerializeField] private Button _playButton;
	[SerializeField] private byte _maxPlayers = 4;
	[SerializeField] private byte _levelIndex = 1;

	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
		_playButton.interactable = PhotonNetwork.IsConnectedAndReady;
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		_playButton.interactable = true;
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		_playButton.interactable = false;
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		int randomName = Random.Range(0, 5000);
		RoomOptions roomOptions = new RoomOptions()
		{
			IsVisible = true,
			IsOpen = true,
			MaxPlayers = _maxPlayers
		};

		PhotonNetwork.CreateRoom($"RoomName_{randomName}", roomOptions);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel(_levelIndex);
	}

	public void FindGame()
	{
		PhotonNetwork.JoinRandomRoom();
	}
}