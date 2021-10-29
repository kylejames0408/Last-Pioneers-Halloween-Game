using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStand : LanternTrigger
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        while(LevelManager.questManager.GetQuestStatus("light houses")=="false")
        {
          //  doLanternTrigger();
        }
    }

    override public void doLanternTrigger()
    {

        if (base.checkStands())
        {
            Debug.Log("poop");
           LevelManager.questManager.UpdateQuest("light houses", "true");
        }
    }
}
