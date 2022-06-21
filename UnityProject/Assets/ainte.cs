using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using TMPro;

public class ainte : MonoBehaviour
{
   
   public void llala()
    {
        GameObject.Find("Canvas/Text (TMP) (2)").GetComponent<TextMeshProUGUI>().text = "MPIKA";
            Instantiate(Resources.Load("Prefabs/Solved") as GameObject);
        //na spawnnaretai s kalo simeio stin camera
        gameObject.transform.parent.gameObject.SetActive(false);
        // GameObject.Find("ScanRiddlePage(Cloned)").SetActive(false);
        StartCoroutine(ieGetProgressPoints());
    }
    IEnumerator ieGetProgressPoints()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("team", GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName);
        dataForm.AddField("thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntName);
        string uri = "https://arthunt.000webhostapp.com/SolvedRiddles.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        string Response = webRequest.downloadHandler.text;

        string[] splitRaw = Response.Split('*');
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
                GameObject.Find("Canvas/Text (TMP) (2)").GetComponent<TextMeshProUGUI>().text= splitRaw[0];
                //GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Progress").GetComponent<TextMeshProUGUI>().text = splitRaw[0];
                GameObject.Find("Canvas/Text (TMP) (1)").GetComponent<TextMeshProUGUI>().text = splitRaw[1];
                //GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Points").GetComponent<TextMeshProUGUI>().text = "Points:  " + splitRaw[1];
                break;
        }
    }
}
