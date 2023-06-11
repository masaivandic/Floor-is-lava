using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private float percentage;
    public Slider Loader;
    public GameObject LoadingScreen;
    public GameObject LOBBY;
    public GameObject SoundManager;


    public void Start()
    {
        Load();
    }



    public void Load()
    {
        StartCoroutine(PercentUp());
    }

    IEnumerator PercentUp()
    {
        do
        {
            yield return new WaitForSeconds(0.03f);
            percentage = percentage + 0.01f;
            Loader.value = percentage;
        }
        while (percentage < 1);

        yield return new WaitUntil(() => percentage >= 1 && PhotonNetwork.IsConnectedAndReady);
        yield return new WaitForSeconds(1.5f);
        LOBBY.SetActive(true);
        SoundManager.transform.GetChild(0).gameObject.transform.GetComponent<AudioSource>().Play();
        LoadingScreen.SetActive(false);
    }
}
