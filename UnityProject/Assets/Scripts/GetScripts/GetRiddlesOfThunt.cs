using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using TMPro;

public class GetRiddlesOfThunt : MonoBehaviour
{
    public List<Riddle> riddles= new List<Riddle>();

    int treasureHunt;

    // Start is called before the first frame update
    void Start()
    {
        treasureHunt = GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID;

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

                    for(int i=0; i<splitRaw.Length-4; i=i+4)
                    {
                        if (splitRaw[i]!= "")
                        {
                            riddle = new Riddle(splitRaw[i], splitRaw[i + 1], splitRaw[i + 2], splitRaw[i + 3]);
                            riddles.Add(riddle);
                        }
                    }

                    GameObject.Find("DataManager").GetComponent<DataManagement>().Riddles = riddles;
                    //string FirstText = riddles.FirstOrDefault().getText();
                Debug.Log("First Text" + riddles.FirstOrDefault());
                  //  GameObject.Find("Dashboard(Clone)/UI/Canvas/Responses/Riddle").GetComponent<TextMeshProUGUI>().text = FirstText;
                    string FirstInfoText = riddles.FirstOrDefault().getText();
                    
                    break;
            }
       
    }
}
