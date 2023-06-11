using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public static Points instance;

    public static int Player1Points ;
    public static int Player2Points;

    public void Start()
    {
        instance = this;
        Player1Points = 3;
        Player2Points = 3;
    }
}
