using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using TMPro;


public class GoDashboard : MonoBehaviour
{
    public GameObject Dashboard;
    public ArrayList riddles;
    public void GoToDashboard()
    {
        
        Instantiate(Resources.Load("Prefabs/Dashboard"));

        riddles = GameObject.Find("DataManager").GetComponent<DataManagement>().Riddles;
        riddles.RemoveAt(0);

        GameObject.Find("DataManager").GetComponent<DataManagement>().Riddles = riddles;
        Riddle r = (Riddle)riddles[0];
        string FirstRiddle = r.getText();
        Debug.Log("First Text" + FirstRiddle);
        GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Riddle").GetComponent<TextMeshProUGUI>().text = FirstRiddle;
        string FirstInfo = r.getInfo();
        GameObject.Find("Solved(Clone)/UI/Canvas/BluePanel/Responses/InfoText").GetComponent<TextMeshProUGUI>().text = FirstInfo;
        GameObject.Find("Dashboard(Clone)/UI/Canvas/BluePanel/Responses/Team Name").GetComponent<TextMeshProUGUI>().text = GameObject.Find("DataManager").GetComponent<DataManagement>().TeamName;

        

    }
   
}

