using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riddle : MonoBehaviour
{
    private string text { get; set; }
    private string location_solution { get; set; }
    private string infotext { get; set; }
    private string points { get; set; }

    public Riddle(string text, string location_solution, string infotext, string points)
    {
        this.text = text;
        this.location_solution = location_solution;
        this.infotext = infotext;
        this.points = points;
    }


    public override string ToString()
    {
        return "Riddle: " + text + " " + location_solution + " " + infotext + " " + points;
    }
}
