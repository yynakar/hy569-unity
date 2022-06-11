using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riddle : MonoBehaviour
{
    private string text { get; set; }
    private string location_solution { get; set; }
    private string object_AR { get; set; }
    private string infotext { get; set; }
    private string points { get; set; }

    public Riddle(string text, string location_solution,string object_AR, string infotext, string points)
    {
        this.text = text;
        this.location_solution = location_solution;
        this.object_AR = object_AR;
        this.infotext = infotext;
        this.points = points;
    }


    public override string ToString()
    {
        return "Riddle: " + text + " " + location_solution + " " + object_AR + " " + infotext + " " + points;
    }
}
