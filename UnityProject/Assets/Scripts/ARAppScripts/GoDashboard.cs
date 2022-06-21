using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using TMPro;


public class GoDashboard : MonoBehaviour
{
    public GameObject Dashboard;
    public ArrayList riddles;
    public void GoToDashboard()
    {


        StartCoroutine(ieGetProgressPoints2());
        GameObject.Find("Solved(Clone)").SetActive(false);
        Instantiate(Resources.Load("Prefabs/Dashboard"));

        riddles = GameObject.Find("DataManager").GetComponent<DataManagement>().Riddles;
        riddles.RemoveAt(0);

        GameObject.Find("DataManager").GetComponent<DataManagement>().Riddles = riddles;
        Riddle r = (Riddle)riddles[0];
        string FirstRiddle = r.getText();
        Debug.Log("First Text" + FirstRiddle);
        GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Riddle").GetComponent<TextMeshProUGUI>().text = FirstRiddle;
        string FirstInfo = r.getInfo();
        GameObject.Find("Solved(Clone)/UI/Canvas/BluePanel/Responses/InfoText").GetComponent<TextMeshProUGUI>().text = FirstInfo;
        GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Team Name").GetComponent<TextMeshProUGUI>().text = GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName;

    }


    IEnumerator ieGetProgressPoints2()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("team", GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName);
        dataForm.AddField("thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntName);
        string uri = "https://arthunt.000webhostapp.com/SolvedRiddles.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        GameObject.Find("Canvas/Text (TMP)").GetComponent<TextMeshProUGUI>().text = "EDO";
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
                GameObject.Find("Canvas/Text (TMP) (2)").GetComponent<TextMeshProUGUI>().text = splitRaw[0];

                //GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Progress").GetComponent<TextMeshProUGUI>().text = splitRaw[0];
                GameObject.Find("Canvas/Text (TMP) (1)").GetComponent<TextMeshProUGUI>().text = splitRaw[1];
                //GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Points").GetComponent<TextMeshProUGUI>().text = "Points:  " + splitRaw[1];
                break;
        }
    }
}
