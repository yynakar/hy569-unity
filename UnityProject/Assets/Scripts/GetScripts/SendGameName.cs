using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendGameName : MonoBehaviour
{
    public string GameName;
    int GameID;
    string TeamName;

    public void SelectGame(GameObject gameName)
    {
        GameName = gameName.ToString().Replace(" (UnityEngine.GameObject)", "");
        StartCoroutine(PostGameName());
        Instantiate(Resources.Load("Prefabs/Dashboard") as GameObject);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator PostGameName()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("name", GameName);
        dataForm.AddField("user", GameObject.Find("DataManager").GetComponent<DataManagement>().LoginResponseUsername);
       
        string uri = "https://arthunt.000webhostapp.com/ReturnThuntIdAndTeam.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        var returnedValues = webRequest.downloadHandler.text;
        Debug.Log(returnedValues);
        string[] values = returnedValues.Split('*');

        if (values[1] != "-1" && values[3] != "-1")
        {
            GameID = int.Parse(values[1]);
            GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID = GameID;

            TeamName = values[3];
            GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName = TeamName;
        }
        else
        {
            Debug.Log("not komple" + values[1]);
            Debug.Log("not komple" + values[3]);
            //minima lathous
            //exception isos
        }
    }
}
