using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartsFunction : MonoBehaviour
{
    Rigidbody rb;
    public Material Red;
    public Material Blue;
    public Material Burnt;
    PhotonView photonView;
    private GameObject Hearts;
    public bool isBurned;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        Hearts = GameObject.FindGameObjectWithTag("Hearts");
        photonView = GetComponent<PhotonView>();
    }

    public void Update()
    {
        if(Points.Player2Points == 0 || Points.Player1Points == 0)
        {
            ChangeColours();
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            if(photonView.IsMine)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                isBurned = true;
                ChangeColours();
                StartCoroutine(WaitBeforeUnfreezing());
                if (PhotonNetwork.IsMasterClient)
                {
                    if (Points.Player1Points != 0)
                    {
                        photonView.RPC("player1Points", RpcTarget.All);
                        Hearts.transform.GetChild(Points.Player1Points).gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (Points.Player2Points != 0)
                    {
                        photonView.RPC("player2Points", RpcTarget.All);
                        Hearts.transform.GetChild(Points.Player2Points).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void player1Points()
    {
        Points.Player1Points--;
    }

    [PunRPC]
    public void player2Points()
    {
        Points.Player2Points--;
    }

    public void ChangeColours()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if(isBurned == true)
            {
                photonView.RPC("Player1Burnt", RpcTarget.All);
            }
            else
            {
                photonView.RPC("Player1Normal", RpcTarget.All);
            }
        }
        else
        {
            if (isBurned == true)
            {
                photonView.RPC("Player2Burnt", RpcTarget.All);
            }
            else
            {
                photonView.RPC("Player2Normal", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void Player2Burnt()
    {
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        player2.transform.GetComponent<MeshRenderer>().material = Burnt;
    }

    [PunRPC]
    public void Player1Burnt()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.transform.GetComponent<MeshRenderer>().material = Burnt;
    }

    [PunRPC]
    public void Player1Normal()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.transform.GetComponent<MeshRenderer>().material = Blue;
    }

    [PunRPC]
    public void Player2Normal()
    {
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        player2.transform.GetComponent<MeshRenderer>().material = Red;
    }

    IEnumerator WaitBeforeUnfreezing()
    {
        GameObject Lava = GameObject.FindGameObjectWithTag("Lava");
        yield return new WaitUntil(() => Lava.transform.position.y < -5.5f); 
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        isBurned = false;
        ChangeColours();
    }
}
