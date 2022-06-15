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
        dataForm.AddField("team","Annoulas Team");
        dataForm.AddField("thunt", "1");
        string uri = "https://arthunt.000webhostapp.com/SolvedRiddles.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        Response = webRequest.downloadHandler.text;
        Debug.Log("Resp" + Response);
        string[] splitRaw = Response.Split('*');

        GameObject.Find("Dashboard(Clone)/UI/Canvas/Responses/Progress").GetComponent<TextMeshProUGUI>().text = splitRaw[0];
        //Debug.Log("Prog" + splitRaw[0]);
        GameObject.Find("Dashboard(Clone)/UI/Canvas/Responses/Points").GetComponent<TextMeshProUGUI>().text ="Points:  "+ splitRaw[1];
        //Debug.Log("Points" + splitRaw[1]);


    }

    void Start()
    {
        StartCoroutine(ieGetProgressPoints());
    }
}
