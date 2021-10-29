using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to store dialogue information.
/// </summary>
[System.Serializable]
public class Dialogue
{
    // Fields
    public string name;
    public bool spokenTo = false;

    [TextArea(3,10)]
    public string[] sentances;
    public string[] sentancesSpokenTo;
}
