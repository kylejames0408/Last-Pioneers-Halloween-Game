using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingZone : MonoBehaviour
{

    //value of the build number of scene to be loaded
    private int roomnumber;

    //determines weather the next or previous room should be loaded
    public bool increasing = true;

    // Start is called before the first frame update
    void Start()
    {
        if(increasing)
        {
            roomnumber = SceneManager.GetActiveScene().buildIndex + 1;
        }
        else
        {
            roomnumber = SceneManager.GetActiveScene().buildIndex - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    /// <summary>
    /// If the collision is with the player, set the canGrab to true on the first hit.
    /// </summary>
    /// <param name="collision">Collision with the object touching it</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            SceneManager.LoadScene(roomnumber);
        }
    }
}
