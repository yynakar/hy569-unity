using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNavigation : MonoBehaviour
{
    public GameObject ScanRiddlePage;
    public void GoToScanRiddlePage()
    {
        Instantiate(ScanRiddlePage);
        gameObject.SetActive(false);
    }   
}
