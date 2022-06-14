using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Solved : MonoBehaviour
{
    int TreasureHunt;
    int TeamName;

    // Start is called before the first frame update
    void Start()
    {
        TreasureHunt = 4;// GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID;
        TeamName = 4;// GameObject.Find("DataManager").GetComponent<DataManagement>().TeamID;
        StartCoroutine(Solve("https://arthunt.000webhostapp.com/download.php?path=qrcodes/riddle_629a69bb14d52f31a58822c0adf0fcd9.png"));
        //StartCoroutine(Solve("https://arthunt.000webhostapp.com/Solved.php?r=4"));
        //auti tha kaleitai sto skanarismeno qr code
    }

    IEnumerator Solve(string uri)
    {

        //WWWForm dataForm = new WWWForm();
        //dataForm.AddField("id_thunt","8"/*treasureHunt*/);
        //dataForm.AddField("id_team", "8"/*team*/);
       
        
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        webRequest.chunkedTransfer = false;


        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

       // string[] pages = uri.Split('/');
        //int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
          //      Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
            //    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // string rawResponse = webRequest.downloadHandler.text;
                //Debug.Log("OPA"+rawResponse);
                var myTexture = webRequest.downloadHandler.data;
                //Texture2D pp = new Texture2D(125,125);
                //pp.LoadRawTextureData(myTexture);
                //// pp.EncodeToPNG();
                //pp.Apply();
                try
                {
                File.WriteAllBytes(Application.dataPath + "/lalalla" + ".png", myTexture);

                }
                catch
                {
                    Debug.Log("hm");
                }
                Debug.Log("File Written On Disk!");
                //display it on dashboard (maybe)

                
                break;
        }

    }
}
