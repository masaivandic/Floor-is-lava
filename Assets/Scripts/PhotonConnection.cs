using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    [Header("Spawnpoints")]
    public GameObject[] spawnPoints;
    public GameObject Lava;
    public GameObject Land;
    public GameObject LavaComing;

    [Header("GameObjects")]
    public GameObject WaitForPlayersScreen;
    public PhotonView photonView;

    public void Start()
    {
        StartCoroutine(Wait());
    }


    IEnumerator Wait()
    {
        yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length > 1 && PhotonNetwork.IsConnectedAndReady);
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject Player1 = PhotonNetwork.Instantiate("PlayerObj", spawnPoints[0].transform.position, Quaternion.identity);
        }
        if(!PhotonNetwork.IsMasterClient)
        {
            GameObject Player2 = PhotonNetwork.Instantiate("PlayerObj", spawnPoints[1].transform.position, Quaternion.identity);
        }
        photonView.RPC("PUNRPCForEnumerator", RpcTarget.All);
    }

    [PunRPC]
    public void PUNRPCForEnumerator()
    {
        LavaComing.SetActive(true);
        WaitForPlayersScreen.SetActive(false);
        Land.SetActive(true);
        Lava.SetActive(true);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameObject PlayerThatIsInRoom = GameObject.FindGameObjectWithTag("Player");
        Lava.SetActive(false);
        Land.SetActive(false) ;
        LavaComing.SetActive(false);
        WaitForPlayersScreen.SetActive(true) ;
        Destroy(PlayerThatIsInRoom);
    }
}
