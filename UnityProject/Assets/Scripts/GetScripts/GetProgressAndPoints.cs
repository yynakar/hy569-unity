using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GetProgressAndPoints : MonoBehaviour
{
    public TMP_Text Progress;
    public TMP_Text Points;
    [NonSerialized]
    public string Response;
    
    IEnumerator ieGetProgressPoints()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("team", "5");
        dataForm.AddField("thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID);
        string uri = "https://arthunt.000webhostapp.com/SolvedRiddles.php";

        UnityEngine.Networking.UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        Response = webRequest.downloadHandler.text;
        Debug.Log("Resp" + Response);
        string[] splitRaw = Response.Split('*');
        Progress.text = splitRaw[0];
        Points.text = splitRaw[1];
        Debug.Log("Prog" + splitRaw[0]);
        Debug.Log("Points" + splitRaw[1]);

    }

    public void GetProgressPoints()
    {
        StartCoroutine(ieGetProgressPoints());
    }
}
