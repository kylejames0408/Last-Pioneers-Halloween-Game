using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles draw order of sprites.
/// </summary>
public class SortScript : MonoBehaviour
{
    // Fields
    public float yhax;

    /// <summary>
    /// Updates the sort order of sprites.
    /// </summary>
    void Update()
    {
        // Calculate sort order
        GetComponent<SpriteRenderer>().sortingOrder = (Mathf.RoundToInt((transform.position.y + yhax) * 100f) * -1);
    }
}