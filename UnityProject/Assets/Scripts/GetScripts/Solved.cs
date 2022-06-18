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
        StartCoroutine(Solve(8));
    }

    IEnumerator Solve(int riddleID)
    {

        WWWForm dataForm = new WWWForm();
        dataForm.AddField("id_thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID);
        dataForm.AddField("team", GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName);//remind katerina na to deite
        string uri ="https://arthunt.000webhostapp.com/Solved.php?r=" + riddleID;


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
                Debug.LogError(UnityWebRequest.Result.DataProcessingError);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(UnityWebRequest.Result.ProtocolError);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(UnityWebRequest.Result.Success);
                break;
        }

    }
}
