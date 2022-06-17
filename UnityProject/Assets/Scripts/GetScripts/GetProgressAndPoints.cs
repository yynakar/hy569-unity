using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GetProgressAndPoints : MonoBehaviour
{
    
    [NonSerialized]
    public string Response;
    
    IEnumerator ieGetProgressPoints()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("team", GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName);
        dataForm.AddField("thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntName);
        Debug.Log("In ieGetProgressPoints: team= " + GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName);
        Debug.Log("In ieGetProgressPoints: thunt= " + GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntName);
        string uri = "https://arthunt.000webhostapp.com/SolvedRiddles.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        Response = webRequest.downloadHandler.text;

        string[] splitRaw = Response.Split('*');

        GameObject.Find("Dashboard(Clone)/UI/Canvas/Responses/Progress").GetComponent<TextMeshProUGUI>().text = splitRaw[0];
        Debug.Log("Prog" + splitRaw[0]);
        GameObject.Find("Dashboard(Clone)/UI/Canvas/Responses/Points").GetComponent<TextMeshProUGUI>().text ="Points:  "+ splitRaw[1];
        Debug.Log("Points" + splitRaw[1]);


    }

    void Start()
    {
        StartCoroutine(ieGetProgressPoints());
    }
}
