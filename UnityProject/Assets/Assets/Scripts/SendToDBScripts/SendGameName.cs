using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ButtonBehavior : MonoBehaviour
{
    string GameName;
    public int GameID;
   public void SelectGame(GameObject gameName)
    {
        Debug.Log("button pressed: "+gameName.ToString().Replace(" (UnityEngine.GameObject)",""));
        GameName = gameName.ToString().Replace(" (UnityEngine.GameObject)", "");
    }

    IEnumerator ieLogin()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("GameName", GameName);
        string uri = "https://arthunt.000webhostapp.com/ReturnThuntId.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        GameID = int.Parse(webRequest.downloadHandler.text);
        Debug.Log("edo1" + GameID);

        if (GameID != -1)
        {
            GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHunt = GameID;
            Debug.Log("edo2" + GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHunt);
            //Dont know yet
        }
        else
        {
            //Dont know yet
        }
    }
}
