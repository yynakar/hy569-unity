using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameName : MonoBehaviour
{
    public TMP_Text game;
    // Start is called before the first frame update
    void Start()
    {
        game.text = GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntName;
    }

}
