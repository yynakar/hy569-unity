using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SendGameName : MonoBehaviour
{
    string GameName;
    int GameID;
    int TeamID;

    public void SelectGame(GameObject gameName)
    {
        GameName = gameName.ToString().Replace(" (UnityEngine.GameObject)", "");
        StartCoroutine(PostGameName());
    }

    IEnumerator PostGameName()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("GameName", GameName);
        dataForm.AddField("UserName", GameObject.Find("DataManager").GetComponent<DataManagement>().LoginResponseUsername);

        string uri = "https://arthunt.000webhostapp.com/ReturnThuntId.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();
        var returnedValues = webRequest.downloadHandler.text;//TreasureHunt*1*Team*1

        string[] values = returnedValues.Split('*');

        if (values[1] != "-1")
        {
            GameID = int.Parse(values[1]);
            GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID = GameID;
            Debug.Log("GameID=" + values[1]);
        }
        else
        {
            Debug.Log("not komple" + values[1]);
            //minima lathous
        }
        if (values[3] != "-1")
        {
            TeamID = int.Parse(values[1]);
            GameObject.Find("DataManager").GetComponent<DataManagement>().TeamID = TeamID;
            Debug.Log("TeamID==" + values[1]);
        }
        else
        {
            Debug.Log("not komple" + values[3]);
            //minima lathous
        }
    }
}
