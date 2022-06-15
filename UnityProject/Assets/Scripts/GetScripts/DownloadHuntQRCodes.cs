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
      //  StartCoroutine(GetQRPaths("https://arthunt.000webhostapp.com/RiddlesPng.php"));
        StartCoroutine(DownloadQRCodes("qrcodes/riddle_t1_r3.png"));

    }

    IEnumerator GetQRPaths(string uri)
    {

        WWWForm dataForm = new WWWForm();
        dataForm.AddField("thunt", GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID);

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
        //var myTexture = webRequest.downloadHandler.data;

       string[] values = paths.Split('*');

       foreach (var i in values)
        {
            UnityWebRequest webRequest2 = UnityWebRequest.Get("https://arthunt.000webhostapp.com/download.php?path=" + i);

        // Request and wait for the desired page.
            yield return webRequest2.SendWebRequest();


            if (i == "")
            {
                break;
            }
            Debug.Log(i);
            //qrcodes/riddle_t1_r1.png*qrcodes/riddle_t1_r2.png*qrcodes/riddle_t1_r3.png*
            // yield return new WaitUntil(() => webRequest2.isDone);

            switch (webRequest2.result)
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
             case UnityWebRequest.Result.InProgress:
                 Debug.Log(UnityWebRequest.Result.InProgress);
                 break;
             case UnityWebRequest.Result.Success:

                 Debug.Log("edo ");
                 break;
         }

        /*switch (webRequest2.result)
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

                var myTexture = webRequest2.downloadHandler.data;
                Debug.Log("arci");
                Debug.Log("i=" + i.Replace("qrcodes/", "").Replace(".png", ""));
                Debug.Log("myTexture=" + myTexture);
                Debug.Log("telos");
                //valta se kalo path
                File.WriteAllBytes(Application.dataPath + "/Resources/" + i.Replace("qrcodes/", "").Replace(".png", "") + ".png", myTexture);
                //Debug.Log("File Written On Disk!");
                break;
        }*/
         }
         //gia prosorina
    }
}
