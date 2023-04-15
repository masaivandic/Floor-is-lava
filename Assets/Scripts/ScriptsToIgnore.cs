using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScriptsToIgnore : MonoBehaviour
{

    public MonoBehaviour[] scriptsToIgnore;
    public PhotonView photonView;

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void Update()
    {
        if(!photonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }
    }
}
