using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadHuntQRCodes : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // string data="";
        StartCoroutine(GetQRPaths("https://arthunt.000webhostapp.com/RiddlesPng.php"));
        //StartCoroutine(DownloadQRCodes("qrcodes/riddle_t1_r3.png"));

    }

    IEnumerator GetQRPaths(string uri)
    {

        WWWForm dataForm = new WWWForm();
        dataForm.AddField("thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntName);
       
        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log(UnityWebRequest.Result.ConnectionError);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log(UnityWebRequest.Result.DataProcessingError);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log(UnityWebRequest.Result.ProtocolError);
                break;
            case UnityWebRequest.Result.Success:

                StartCoroutine(DownloadQRCodes(webRequest.downloadHandler.text));

                break;

        }
    }

    IEnumerator DownloadQRCodes(string paths)
    {
       

        string[] values = paths.Split('*');

        for (int i = 0; i < values.Length; i++)

            using (UnityWebRequest webRequest = UnityWebRequest.Get("https://arthunt.000webhostapp.com/download.php?path=" + values[i]))
            {
                if (values[i] == "")
                {
                    break;
                }
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(" HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log("Success: " + i);
                        var myTexture = webRequest.downloadHandler.data;
                        File.WriteAllBytes(Application.dataPath + "/Resources/" + values[i].Replace("qrcodes/", "").Replace(".png", "") + ".png", myTexture);
                        break;
                }
            }
        }
    
}
        