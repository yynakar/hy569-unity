using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDashboard : MonoBehaviour
{
    public GameObject Dashboard;
    // public GameObject ARFoundation;
    public void GoToDashboard()
    {
        Instantiate(Resources.Load("Prefabs/Dashboard"));
        //gameObject.SetActive(false);
    }
}
