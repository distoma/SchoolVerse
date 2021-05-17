using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Menu : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void SearchForGame()
    {
        isConnecting = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 연결됨...");
        // base.OnConnectedToMaster();

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"연결되지 않은 이유: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방이 생성되지 않았습니다...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room 접속이 성공하였습니다..");
        PhotonNetwork.LoadLevel("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
