using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ainte : MonoBehaviour
{
   public void llala()
    {
        GameObject.Find("Canvas/Text (TMP) (2)").GetComponent<TextMeshProUGUI>().text = "MPIKA";
            Instantiate(Resources.Load("Prefabs/Solved") as GameObject);
        //na spawnnaretai s kalo simeio stin camera
        gameObject.transform.parent.gameObject.SetActive(false);
       // GameObject.Find("ScanRiddlePage(Cloned)").SetActive(false);
    }
}
