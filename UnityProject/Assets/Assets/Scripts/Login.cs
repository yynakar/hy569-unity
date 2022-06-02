using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net.Http;
public class Login : MonoBehaviour
{
    public InputField inputUsername;
    public InputField inputPassword;
    public Text response;
    private static readonly HttpClient client = new HttpClient();

    IEnumerator ieLogin()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("userName", inputUsername.text);
        dataForm.AddField("passWord", inputPassword.text);
        string uri = "http://arthunt.epizy.com/Login.php";
        // System.Net.Http.HttpClient
        //HttpClient
        //System.Net.Http.HttpClient webRequest = new System.Net.Http.HttpClient();
        //webRequest.PostAsync(uri, dataForm);
        //webRequest.chunkedTransfer = false;
        var values = new Dictionary<string, string>
          {
              { "userName", inputUsername.text },
              { "passWord", inputPassword.text }
          };
        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("http://arthunt.epizy.com/Login.php", content);

        yield return webRequest.SendWebRequest();

        //Debug.Log(webRequest.downloadHandler.text);
        //response.text = "Res: " + webRequest.downloadHandler.text +".";
        response.text = webRequest.error;

    }

    public void login()
    {
        StartCoroutine(ieLogin());
    }
}
