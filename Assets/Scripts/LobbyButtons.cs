using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyButtons : MonoBehaviourPunCallbacks
{
    public void JoinGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
