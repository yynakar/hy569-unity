using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadHuntQRCodes : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Solve("https://arthunt.000webhostapp.com/download.php?path=qrcodes/riddle_629a69bb14d52f31a58822c0adf0fcd9.png")); 
    }

    IEnumerator Solve(string uri)
    {

        WWWForm dataForm = new WWWForm();

        UnityWebRequest webRequest = UnityWebRequest.Post(uri,dataForm);
        webRequest.chunkedTransfer = false;


        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        // string[] pages = uri.Split('/');
        //int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                break;
            case UnityWebRequest.Result.ProtocolError:
                break;
            case UnityWebRequest.Result.Success:
                var myTexture = webRequest.downloadHandler.data;
                try
                {
                    File.WriteAllBytes(Application.dataPath + "/lalalla" + ".png", myTexture);
                }
                catch
                {
                    Debug.Log("hm");
                }
                Debug.Log("File Written On Disk!");
                break;
        }

    }
}
