using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PageNavigation : MonoBehaviour
{
    public GameObject ScanRiddlePage;
   // public GameObject ARFoundation;
    public void GoToScanRiddlePage()
    {
        Instantiate(ScanRiddlePage);
        gameObject.SetActive(false);
    }   
}
