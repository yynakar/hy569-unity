using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetRiddlesOfThunt : MonoBehaviour
{
    public List<Riddle> riddles= new List<Riddle>();

    string loginResponseUsername;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetRequest("https://arthunt.000webhostapp.com/ReturnRiddleThunt.php"));
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

                    string rawResponse = webRequest.downloadHandler.text;
                    string[] splitRaw= rawResponse.Split('*');
                    Riddle riddle;

                    for(int i=0; i<splitRaw.Length-5; i=i+5)
                    {
                        if (splitRaw[i]!= "")
                        {
                            riddle = new Riddle(splitRaw[i], splitRaw[i + 1], splitRaw[i + 2], splitRaw[i + 3], splitRaw[i + 4]);
                            riddles.Add(riddle);
                        }
                    }

                    foreach (Riddle element in riddles)
                    {
                        Debug.Log($"{element.ToString()} ");
                    }

                    break;
            }
        }
    }
}
