using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNavigation : MonoBehaviour
{
    public GameObject ScanRiddlePage;
    public GameObject AuthMessage;

    public void GoToScanRiddlePage(GameObject currentPage)
    {
        currentPage.SetActive(false);
        ScanRiddlePage.SetActive(true);
    }



}
