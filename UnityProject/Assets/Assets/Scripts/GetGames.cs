using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetGames : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //na mpei to usename kapws edw
        StartCoroutine(GetRequest("https://arthunt.000webhostapp.com/Game.php?username=Maria"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
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
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    string rawResponse = webRequest.downloadHandler.text;
                    string[] games = rawResponse.Split('*');
                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i] != "")
                        {
                            Debug.Log("Current game:" + games[i]);
                        }                     
                    }

                    break;
            }
        }
    }
}
