using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    public InputField inputUsername;
    public InputField inputPassword;
    public Text response;
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

        //Debug.Log(webRequest.downloadHandler.text);
        response.text = "Res: " + webRequest.downloadHandler.text + ".";

    }

    public void login()
    {
        StartCoroutine(ieLogin());
    }
}
