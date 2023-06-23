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
    public GameObject Instructions;
    int index = 0;
    public void JoinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HowToPlay()
    {
        Instructions.SetActive(true);
        Instructions.transform.GetChild(1).gameObject.SetActive(false);
        Instructions.transform.GetChild(index).gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitInstructions()
    {
        Instructions.SetActive(false);
        index = 0;
    }

    public void Next()
    {
        Instructions.transform.GetChild(index).gameObject.SetActive(false);
        index++;
        Instructions.transform.GetChild(index).gameObject.SetActive(true);
    }
}
