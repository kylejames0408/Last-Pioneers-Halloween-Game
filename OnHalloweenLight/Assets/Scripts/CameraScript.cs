using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //centers camera on player
        // this SHOULD be able to be done by just making the camera  child of the player but
        // whenever i did that the camera wouldnt view anything because of z axis stuff and literally
        // nothing i tried fixed it so i just did it within a script
        transform.position = LevelManager.player.transform.position + new Vector3(0, 0, -20);
    }
}
