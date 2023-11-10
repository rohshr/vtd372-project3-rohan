using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;

namespace Assets.p3.scripts.pun
{
	public class photonLauncher : MonoBehaviourPunCallbacks
	{
		#region Public Variables
		[Tooltip("name of the netowrk room to use, Default = sampleroom")]
		public string roomName = "VTD372";
		[Tooltip("Game Version number, Default = 1.0")]
		public string _gameVersion = "1.0";
		[Tooltip("Maximum number of Player for this room, Default = 10, cannot exceed 20")]
		public byte maxPlayers = 10;
		//[Tooltip("set active if your are debugging Photon Setup and need simple character instantiation")]
		//public bool isDebug = false;
		//[Tooltip("Name of Character Prefab - only used if in Debug mode")]
		//public string debugCharacter;
		//[Tooltip("Spawn Point - only used if in Debug mode")]
		//public Transform debugSpawnPoint;
		#endregion


		#region Private Variables
		/// <summary>
		/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
		/// </summary>

		#endregion


		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		void Awake()
		{
			// #NotImportant
			// Force Full LogLevel
			//PhotonNetwork.LogLevel = PhotonLogLevel.Full;

			// #Critical
			// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
			//PhotonNetwork.autoJoinLobby = false;

			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.AutomaticallySyncScene = true;
		}
			
		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
            //set the default room name
            if (roomName == "")
                roomName = "VTD372";
            //set the default max players
            if (maxPlayers == 0)
				maxPlayers = 10;
			if (maxPlayers > 20)
				maxPlayers = 20;
			//set the default game version number
			if (_gameVersion == "")
				_gameVersion = "1.0";
			// call the connect function
			Connect();
		}
			
		#endregion


		#region Public Methods

		/// <summary>
		/// Start the connection process.
		/// - If already connected, we attempt joining a random room
		/// - if not yet connected, Connect this application instance to Photon Cloud Network
		/// </summary>
		public void Connect()
		{
			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.IsConnected)
			{
				Debug.Log("Joining Room...");
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
				PhotonNetwork.JoinRandomRoom();
			}else{

				Debug.Log("Connecting...");

				// #Critical, we must first and foremost connect to Photon Online Server.
				//PhotonNetwork.GameVersion = this.gameVersion;
				PhotonNetwork.GameVersion = _gameVersion;
				PhotonNetwork.ConnectUsingSettings();
			}
		}


		#endregion

		#region MonoBehaviourPunCallbacks CallBacks
		// below, we implement some callbacks of PUN
		// you can find PUN's callbacks in the class MonoBehaviourPunCallbacks



		public override void OnConnectedToMaster()
		{
			//Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
			//*********************************************
			// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
			// For a random room
			//PhotonNetwork.JoinRandomRoom();
			//*********************************************
			//For a specific room
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.IsVisible = false;
			roomOptions.MaxPlayers = maxPlayers;
			//PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers});
			PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
		}
		public override void OnJoinedRoom()
		{
//			Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
			Debug.Log ("***************** MU01 Room Name: " + PhotonNetwork.CurrentRoom.ToStringFull());
			Debug.Log ("***************** MU01 Max Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
			//next line only fires in Debug mode
			//if(isDebug) PhotonNetwork.Instantiate( debugCharacter, debugSpawnPoint.position, Quaternion.identity, 0 );
		}

		#endregion

	}
}
