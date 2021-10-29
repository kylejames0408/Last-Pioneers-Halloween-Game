using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Updates a Camera to follow the Player defined in the LevelManager.
/// </summary>
public class CameraScript : MonoBehaviour
{
    /// <summary>
    /// Updates the Camera's position.
    /// </summary>
    void Update()
    {
        // Centers the camera on the Player
        transform.position = LevelManager.player.transform.position + new Vector3(0, 0, -20);
    }
}
