using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    [Header(";3")]
    public GameObject[] spawnPoints;
    public PhotonView photonView;
    public new List<GameObject> objectsToDisable = new List<GameObject>();
    public new List<GameObject> objectsToEnable = new List<GameObject>();

    public void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        }
        StartCoroutine(Wait());
    }


    IEnumerator Wait()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length > 1);
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject Player1 = PhotonNetwork.Instantiate("Player1", spawnPoints[0].transform.position, Quaternion.identity);
        }
        if(!PhotonNetwork.IsMasterClient)
        {
            GameObject Player2 = PhotonNetwork.Instantiate("Player2", spawnPoints[1].transform.position, Quaternion.identity);
        }
        ObjectEnabling();
    }

    public void ObjectEnabling()
    {
        foreach(var objToEnable in objectsToEnable)
        {
            objToEnable.SetActive(true);
        }
        foreach (var objToDisable in objectsToDisable)
        {
            objToDisable.SetActive(false);
        }
        objectsToEnable[1].gameObject.GetComponent<LavaFunction>().enabled = true;
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach (var objToEnable in objectsToEnable)
        {
            objToEnable.SetActive(false);
        }
        foreach (var objToDisable in objectsToDisable)
        {
            objToDisable.SetActive(true);
        }
        objectsToDisable[0].SetActive(false);
        objectsToEnable[1].gameObject.GetComponent<LavaFunction>().enabled = false;
        objectsToEnable[1].gameObject.transform.position = new Vector3(-8.7f, -8.0f, 4.25f);
        StartCoroutine(Wait());
    }
}
