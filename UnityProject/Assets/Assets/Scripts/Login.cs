using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class Login : MonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_InputField inputPassword;
    public TMP_Text DebugText;
    public GameObject Dashboard;
    public GameObject BadAuthMesg;

    [NonSerialized]
    public string LoginResponseUsername;

    IEnumerator ieShowTreasureHunt()
    {
        
        string uri = "https://arthunt.000webhostapp.com/Game.php?username="+LoginResponseUsername;

        yield return 0;
        /* UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        LoginResponseUsername = webRequest.downloadHandler.text;

        if (LoginResponseUsername != "0")
        {
        Instantiate(Dashboard);
        gameObject.SetActive(false);
        // DebugText.text = LoginResponseUsername;
        }
        else
        {
        Instantiate(BadAuthMesg);
        }*/
    }

    public void showTreasureHunt()
    {
        StartCoroutine(ieShowTreasureHunt());
    }

    IEnumerator ieLogin()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("userName", inputUsername.text);
        dataForm.AddField("passWord", inputPassword.text);
        string uri = "https://arthunt.000webhostapp.com/Login.php";

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        LoginResponseUsername = webRequest.downloadHandler.text;

        if (LoginResponseUsername != "0")
        {
            Instantiate(Dashboard);
            gameObject.SetActive(false);
            // DebugText.text = LoginResponseUsername;
        }
        else
        {
            Instantiate(BadAuthMesg);
        }
    }

    public void login()
    {
        StartCoroutine(ieLogin());
    }
}
