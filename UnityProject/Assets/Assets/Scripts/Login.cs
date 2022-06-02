using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    public TextMesh inputUsername;
    public InputField inputPassword;
    
    IEnumerator ieLogin()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("userName", inputUsername.text);
        dataForm.AddField("passWord", inputPassword.text);
        string uri = "http://arthunt.epizy.com/Login.php";
        UnityWebRequest webRequest = UnityWebRequest.Post(uri,dataForm);
        webRequest.chunkedTransfer = false;

        yield return webRequest.SendWebRequest();

        Debug.Log(webRequest.downloadHandler.text);

    }

    public void login()
    {
        StartCoroutine(ieLogin());
    }
}
