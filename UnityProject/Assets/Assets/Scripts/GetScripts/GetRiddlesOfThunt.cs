using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetRiddlesOfThunt : MonoBehaviour
{
    public List<Riddle> riddles= new List<Riddle>();

    int treasureHunt;

    // Start is called before the first frame update
    void Start()
    {
        treasureHunt = GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHunt;
        Debug.Log(treasureHunt);
        StartCoroutine(GetRequest("https://arthunt.000webhostapp.com/ReturnRiddleThunt.php"));
    } 

    IEnumerator GetRequest(string uri)
    {
        
            WWWForm dataForm = new WWWForm();
            dataForm.AddField("thunt", treasureHunt);//bale sto 20 to global thing


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
