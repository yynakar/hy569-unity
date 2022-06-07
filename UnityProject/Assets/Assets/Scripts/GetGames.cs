using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetGames : MonoBehaviour
{
    public GameObject GameContainer;
    public GameObject GameTemplate;

    string loginResponseUsername;

    // Start is called before the first frame update
    void Start()
    {
        loginResponseUsername = GameObject.Find("DataManager").GetComponent<DataManagement>().LoginResponseUsername;
        //na mpei to usename kapws edw
        //StartCoroutine(GetRequest("https://arthunt.000webhostapp.com/Game.php?username="+ loginResponseUsername));
        StartCoroutine(GetRequest("https://arthunt.000webhostapp.com/Game.php?username=Maria"));
        //StartCoroutine(GetRequest("https://arthunt.000webhostapp.com/Game.php?username=kate"));
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
                    string[] games = rawResponse.Split('*');
                    foreach(string i in games)
                    {
                        if (i != "")
                        {
                            GameObject ga = Instantiate(GameTemplate,GameContainer.transform);
                            ga.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                            ga.GetComponent<GameName>().game.text = i;
                            ga.name = i;
                        }
                    }
                    GameTemplate.SetActive(false);
                    break;
            }
        }
    }
}
