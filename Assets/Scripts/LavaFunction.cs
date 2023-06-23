using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;


public class LavaFunction : MonoBehaviour
{
    public GameObject Lava;
    public GameObject Land;
    public float speed;
    public float LandSpeed;
    public GameObject LavaComingIn;
    Rigidbody Landrb;
    Rigidbody Lavarb;
    public BoxCollider[] componentslist;
    public bool ChangeText;
    public bool landUp = true;
    public bool lavaUp = true;

    public void Start()
    {
        Lavarb = Lava.GetComponent<Rigidbody>();
        Landrb = Land.GetComponent<Rigidbody>();
        Landrb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
    }
    public void Update()
    {
        LavaRise();
    }
    public void LavaRise()
    {
        if (Land.transform.position.y < -6.4f && landUp == true)
        {
            Landrb.velocity = Vector3.up * LandSpeed;
            ChangeText = true;
        }
        else
        {
            landUp = false;
            if (Lava.transform.position.y < -4.1f && lavaUp == true)
            {
                LavaTextUpdate();
                ChangeText = false;
                Lavarb.velocity = Vector3.up * speed;
                Lava.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                
            }
            else
            {
                lavaUp = false;
                StartCoroutine(LavaDown());
                Lavarb.constraints = RigidbodyConstraints.FreezeAll;
            }
            Landrb.constraints = RigidbodyConstraints.FreezeAll;
            foreach (var boxcollider in componentslist)
            {
                boxcollider.isTrigger = false;
            }
        }
    }

    public void LavaTextUpdate()
    {
        StartCoroutine(LavaText());
    }


    IEnumerator LavaText()
    {
        if(ChangeText == true && Land.transform.position.y >= -6.4f)
        {
            TextMeshProUGUI lavaText = LavaComingIn.transform.Find("LavaComingIn").gameObject.GetComponent<TextMeshProUGUI>();
            lavaText.text = "Lava coming in... 3";

            yield return new WaitForSeconds(0.5f);
            lavaText.text = "Lava coming in... 2";

            yield return new WaitForSeconds(0.5f);
            lavaText.text = "Lava coming in... 1";

            yield return new WaitForSeconds(0.5f);
            lavaText.text = "LAVA IS COMING!!!";
        }
    }

    IEnumerator LavaDown()
    {
        yield return new WaitForSeconds(5);
        Lavarb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Lavarb.velocity = Vector3.down * speed;
        yield return new WaitForSeconds(1);
        lavaUp = true;
        Lavarb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(1);
        foreach (var boxcollider in componentslist)
        {
            boxcollider.isTrigger = true;
        }
        Landrb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Landrb.velocity = Vector3.down * LandSpeed;
        yield return new WaitUntil(() => Land.transform.position.y <= -9.4f);
        landUp = true;
    }
}
