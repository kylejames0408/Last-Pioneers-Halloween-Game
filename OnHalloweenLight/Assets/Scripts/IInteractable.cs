using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for interactable objects.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Do something on interaction.
    /// </summary>
    void DoSomething();

    /// <summary>
    /// Update the sprite.
    /// </summary>
    void SpriteUpdate();
}