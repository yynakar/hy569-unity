using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
   public void SelectGame(GameObject gameName)
    {
        Debug.Log(gameName.ToString().Replace(" (UnityEngine.GameObject)",""));
        //send to base
    }
}
