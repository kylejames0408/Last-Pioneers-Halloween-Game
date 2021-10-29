using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall :  LanternTrigger
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        doLanternTrigger();
    }

    override public void doLanternTrigger()
    {

        if(base.checkStands())
        {
            Disable();
            if (questUpdate && LevelManager.questManager.GetQuestStatus("light houses")=="false")
            {
                LevelManager.questManager.UpdateQuest("light houses", "true");
            }
        }
        else
        {
            Enable();
        }
    }


    void Enable()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Disable()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
