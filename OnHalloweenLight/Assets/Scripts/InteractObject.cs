using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for interactable objects.
/// </summary>
public class InteractObject : MonoBehaviour, IInteractable
{
    // Fields
    private float xDifference;
    private float yDifference;
    protected bool isLantern;
    protected SpriteRenderer sprite;
    public Sprite spriteNoLight;
    public Sprite spriteWithLight;
    public bool touchingLight;

    /// <summary>
    /// Initializes InteractObject fields.
    /// </summary>
    void Start()
    {
        // Initialize fields
        sprite = GetComponent<SpriteRenderer>();
        isLantern = false;
        touchingLight = false;
    }

    /// <summary>
    /// Do something every frame.
    /// </summary>
    void Update()
    {
        // Do something.
        DoSomething();
    }

    /// <summary>
    /// Do something on interaction.
    /// </summary>
    public virtual void DoSomething()
    {
    }

    /// <summary>
    /// Update the sprite.
    /// </summary>
    public virtual void SpriteUpdate()
    {
    }
}