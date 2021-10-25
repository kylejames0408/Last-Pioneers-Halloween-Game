using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public bool spokenTo = false;

    [TextArea(3,10)]
    public string[] sentances;
    public string[] sentancesSpokenTo;
}
