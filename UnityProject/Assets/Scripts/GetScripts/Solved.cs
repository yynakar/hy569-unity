using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Solved : MonoBehaviour
{
    public TMP_Text debugText;
    public TMP_Text debugText2;
    private void Start()
    {
        debugText = GameObject.Find("Canvas/Text (TMP)").GetComponent<TextMeshProUGUI>();
        debugText2 = GameObject.Find("Canvas/Text (TMP) (1)").GetComponent<TextMeshProUGUI>();
    }
    public void cSolved(int i)
    {
        StartCoroutine(Solve(i));
    }
    IEnumerator Solve(int riddleID)
    {

        WWWForm dataForm = new WWWForm();
        dataForm.AddField("id_thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID);
        dataForm.AddField("team",GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName);
        string uri ="https://arthunt.000webhostapp.com/Solved.php?r=" + riddleID;

        //UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm); 
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm))
        {
            webRequest.chunkedTransfer = false;


            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            debugText.text = "req";
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
                    GameObject.Find("DataManager").GetComponent<DataManagement>().riddleSolved = true;
                    ValidateQR();
                    break;
            }


        }
    }
    public void ValidateQR()
    {
        debugText2.text = "edo " + GameObject.Find("DataManager").GetComponent<DataManagement>().riddleSolved;

        if (GameObject.Find("DataManager").GetComponent<DataManagement>().riddleSolved == true)
        {
            Instantiate(Resources.Load("Prefabs/btn_Proceed"), GameObject.Find("ScanRiddlePage(Clone)/UI/Canvas/").transform);
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/RiddleScanFailed"));
        }
    }

}
