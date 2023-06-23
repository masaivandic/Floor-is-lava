using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class Points : MonoBehaviour
{
    public static Points instance;

    public static int Player1Points ;
    public static int Player2Points;
    public GameObject EndOfRoundText;
    PhotonView photonView;
    public new List<GameObject> objectsToDisable = new List<GameObject>();


    public void Update()
    {
        if(Player2Points == 0 || Player1Points == 0)
        {
            photonView.RPC("PointCounter", RpcTarget.All);
        }
    }
    public void Start()
    {
        photonView = GetComponent<PhotonView>();
        instance = this;
        Player1Points = 3;
        Player2Points = 3;
    }

    [PunRPC]
    public void PointCounter()
    {
        if (Player1Points == 0)
        {
            EndOfRoundText.GetComponent<TextMeshProUGUI>().text = "Red wins! Loading new round!";
        }
        else if(Player2Points == 0)
        {
            EndOfRoundText.GetComponent<TextMeshProUGUI>().text = "Blue wins! Loading new round!";
        }
        else if(Player2Points == 0 && Player1Points == 0)
        {
            EndOfRoundText.GetComponent<TextMeshProUGUI>().text = "It's a tie! Loading new round!";
        }
        objectsToDisable[0].transform.position = new Vector3(-9.3f, -6.4f, 1);
        objectsToDisable[2].transform.position = new Vector3(-8.7f, -8.0f, 4.25f);
        objectsToDisable[2].GetComponent<LavaFunction>().enabled = false;
        objectsToDisable[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
        EndOfRoundText.SetActive(true);
        StartCoroutine(LoadingNewRound());
        Player1Points = 3;
        Player2Points = 3;
    }

    IEnumerator LoadingNewRound()
    {
        yield return new WaitForSeconds(5);
        objectsToDisable[2].GetComponent<LavaFunction>().enabled = true;
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
        EndOfRoundText.SetActive(false);
    }
}
