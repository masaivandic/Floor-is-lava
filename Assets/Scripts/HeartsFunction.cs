using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartsFunction : MonoBehaviour
{
    Rigidbody rb;
    public Material Green;
    public ExitGames.Client.Photon.Hashtable _healthCount = new ExitGames.Client.Photon.Hashtable();
    public Material Burnt;
    public GameObject lava;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(WaitBeforeUnfreezing());
            transform.GetComponent<MeshRenderer>().material.color = Color.black;
        }

    }

    IEnumerator WaitBeforeUnfreezing()
    {
        GameObject Lava = GameObject.FindGameObjectWithTag("Lava");
        yield return new WaitUntil(() => Lava.transform.position.y < -5.5f);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
