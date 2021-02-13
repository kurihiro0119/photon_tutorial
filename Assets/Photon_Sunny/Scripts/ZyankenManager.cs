using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ZyankenManager : MonoBehaviourPunCallbacks
{
    public string myStatus;
    public string enemyStatus = null;

    public void gooSet()
    {

        if (photonView.IsMine)
        {
            myStatus = "goo";
        }
        else
        {
            enemyStatus = "goo";
        }

        if(myStatus == "goo" && enemyStatus != null)
        {
            switch (enemyStatus)
            {
                case "goo":
                    Debug.Log("hikiwake");
                    break;
                case "pa":
                    Debug.Log("lose");
                    break;
                case "tyoki":
                    Debug.Log("win");
                    break;
            }
        }
    }

    public void tyokiSet()
    {

        if (photonView.IsMine)
        {
            myStatus = "tyoki";
        }
        else
        {
            enemyStatus = "tyoki";
        }

        if (myStatus == "tyoki" && enemyStatus != null)
        {
            switch (enemyStatus)
            {
                case "goo":
                    Debug.Log("lose");
                    break;
                case "pa":
                    Debug.Log("win");
                    break;
                case "tyoki":
                    Debug.Log("hikiwake");
                    break;
            }
        }
    }

    public void paSet()
    {

        if (photonView.IsMine)
        {
            myStatus = "pa";
        }
        else
        {
            enemyStatus = "pa";
        }

        if (myStatus == "pa" && enemyStatus != null)
        {
            switch (enemyStatus)
            {
                case "goo":
                    Debug.Log("win");
                    break;
                case "pa":
                    Debug.Log("hikiwake");
                    break;
                case "tyoki":
                    Debug.Log("lose");
                    break;
            }
        }
    }

}
