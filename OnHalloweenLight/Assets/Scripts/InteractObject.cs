using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    private float xDifference;
    private float yDifference;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xDifference = player.transform.position.x - transform.position.x;
        yDifference = player.transform.position.y - transform.position.y;

        // So these values will have to be changed once the two things get actual sprites. Also could probably be managed from a game manager? Not sure tho...
        if(xDifference > -.5 && xDifference < .5) //Making sure the player x is within .5 units from the thisObject x
        {
            if (yDifference > -.5 && yDifference < .5) //Making sure the player y is within .5 units from the thisObject y
            {
                Debug.Log("This is like close enough to interact I guess.");
            }
        }

        #region Proximity testing. Feel free to ignore :)
        /*
        if (xDifference > .5)
        {
            if (yDifference > .5)
            {
                Debug.Log("The player is to the right and above of the interactObject");
            }
            else if (yDifference < -.5)
            {
                Debug.Log("The player is to the right and below of the interactObject?");
            }
            else
            {
                Debug.Log("The player is to the right and at the same y as the interactObject");
            }
        }
        else if (xDifference < -.5)
        {
            if (yDifference > .5)
            {
                Debug.Log("The player is to the left and above of the interactObject");
            }
            else if (yDifference < -.5)
            {
                Debug.Log("The player is to the left and below of the interactObject");
            }
            else
            {
                Debug.Log("The player is to the left and at the same y as the interactObject");
            }
        }
        else
        {
            if (yDifference > .5)
            {
                Debug.Log("The player is above of the interactObject and at the same x");
            }
            else if (yDifference < -.5)
            {
                Debug.Log("The player is below of the interactObject and at the same x");
            }
            else
            {
                Debug.Log("The player is at the same x and y as the interactObject");
            }
        }
        */
        #endregion
    }

    public virtual void DoSomething()
    {

    }
}
