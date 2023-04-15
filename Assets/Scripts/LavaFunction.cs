using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class LavaFunction : MonoBehaviour
{
    public GameObject Lava;
    public GameObject Land;
    public float speed;
    public float LandSpeed;
    public GameObject LavaComingIn;
    Rigidbody rb;
    public bool LavaTextChanging;
    public BoxCollider[] componentslist;
    public bool timeToSpawn;

    public void Start()
    {
        InvokeRepeating("LavaTextUpdate", 0.5f, 19);
    }
    public void Update()
    {
        if(gameObject.activeInHierarchy == true)
        {
            LavaRise();
        }
    }
    public void LavaRise()
    {
        rb = Lava.GetComponent<Rigidbody>();
        if (Land.transform.position.y < -2.8f)
        {
            LavaTextChanging = true;
            Land.GetComponent<Rigidbody>().velocity = Vector3.up * LandSpeed;
            foreach (var boxcollider in componentslist)
            {
                boxcollider.isTrigger = true;
            }
        }
        else
        {
            foreach (var boxcollider in componentslist)
            {
                boxcollider.isTrigger = false;
            }
            Land.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (Lava.transform.position.y < -4.1f)
        {
            rb.velocity = Vector3.up * speed;
        }
        else
        {
            StartCoroutine(LavaDown());
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void LavaTextUpdate()
    {
        StartCoroutine(LavaText());
    }


    IEnumerator LavaText()
    {
        if(gameObject.activeInHierarchy == true)
        {
            if (LavaTextChanging == true)
            {
                TextMeshProUGUI lavaText = LavaComingIn.transform.Find("LavaComingIn").gameObject.GetComponent<TextMeshProUGUI>();
                lavaText.text = "Lava coming in... 3";

                yield return new WaitForSeconds(1);
                lavaText.text = "Lava coming in... 2";

                yield return new WaitForSeconds(1);
                lavaText.text = "Lava coming in... 1";

                yield return new WaitForSeconds(1);
                lavaText.text = "LAVA IS COMING!!!";
                LavaTextChanging = false;
            }
        }
    }

    IEnumerator LavaDown()
    {
        yield return new WaitForSeconds(5);
        rb.velocity = Vector3.down * 1;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        yield return new WaitForSeconds(2);
        rb.velocity = Vector3.down * speed;
        foreach (var boxcollider in componentslist)
        {
            boxcollider.isTrigger = true;
        }
        Land.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Land.GetComponent<Rigidbody>().velocity = Vector3.down * LandSpeed;
    }
}
