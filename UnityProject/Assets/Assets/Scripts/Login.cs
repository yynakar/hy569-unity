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
    public Text DebugText;
    public GameObject Dashboard;

    [NonSerialized]
    public string LoginResponseUsername;
    IEnumerator ieLogin()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("userName", inputUsername.text);
        dataForm.AddField("passWord", inputPassword.text);
        string uri = "https://arthunt.000webhostapp.com/Login.php";

        //string uri = "http://localhost/CS569-WebApplication/htdocs/Login.php";
        UnityWebRequest webRequest = UnityWebRequest.Post(uri, dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        LoginResponseUsername = webRequest.downloadHandler.text;
        if (LoginResponseUsername != "0")
        {
            gameObject.SetActive(false);
            Instantiate(Dashboard);
        }
        else
        {
            DebugText.text = "Bad luck";
        }
    }

    public void login()
    {
        StartCoroutine(ieLogin());
    }
}
