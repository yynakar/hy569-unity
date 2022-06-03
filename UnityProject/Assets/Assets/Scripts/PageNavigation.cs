using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNavigation : MonoBehaviour
{
  

    public void GoToScanRiddlePage(GameObject ScanRiddlePage)
    {
        gameObject.SetActive(false);
        ScanRiddlePage.SetActive(true);
    }

   
}
