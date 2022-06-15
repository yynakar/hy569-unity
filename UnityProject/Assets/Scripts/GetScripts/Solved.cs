using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Solved : MonoBehaviour
{
    int treasureHunt;
    int team;

    // Start is called before the first frame update
    void Start()
    {
        treasureHunt = 4;// GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID;
        team = 4;// GameObject.Find("DataManager").GetComponent<DataManagement>().TeamID;
        StartCoroutine(Solve("https://arthunt.000webhostapp.com/Solved.php?r=4"));
    }

    IEnumerator Solve(string uri)
    {

        WWWForm dataForm = new WWWForm();
        dataForm.AddField("id_thunt", "8"/*treasureHunt*/);
        dataForm.AddField("id_team", "8"/*team*/);


        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;


        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:

                string rawResponse = webRequest.downloadHandler.text;
                Debug.Log(rawResponse);
                break;
        }

    }
}
