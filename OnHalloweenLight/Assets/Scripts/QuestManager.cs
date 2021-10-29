using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    protected string[,] Quests;
    void Start()
    {
        Quests = new string[3, 2];

        //Quests
        Quests[0, 0] = "pumpkin patch";
        Quests[0, 1] = "false";

        Quests[1, 0] = "light houses";
        Quests[1, 1] = "false";

        Quests[2, 0] = "annoucement at midnight";
        Quests[2, 1] = "false";
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {
            case GameState.Game:
                if (Crow.crowCount == 0)
                {
                    UpdateQuest("pumpkin patch", "true");
                }
                break;
        }
              
    }

    public string GetQuestStatus(string name)
    {
        for (int i = 0; i < Quests.Length / 2; i++)
        {
            if (Quests[i, 0].ToLower() == name)
            {
                return Quests[i, 1];
            }
        }
        return "Quest status not pulled. Check the name.";
    }

    public string GetQuestName(int num)
    {
        if (num < 3 && num >= 0)
        {
            //Debug.Log("Printing quest " + num);
            return Quests[num, 0];
        }
        return "Quest request not valid";
    }

    public void UpdateQuest(string name, string status)
    {
        for(int i = 0; i < Quests.Length / 2; i++)
        {
            //Debug.Log(Quests.Length);
            if (Quests[i, 0].ToLower() == name)
            {
                if(status.ToLower() == "true" || status.ToLower() == "false")
                {
                    Quests[i, 1] = status.ToLower();
                } else
                {
                    Debug.Log("Quest status not updated. Please input a proper status.");
                }
            } else
            {
                //Debug.Log("Quest status not updated. Please input a proper quest name.");
            }
        }
    }

    public bool CheckFinalQuest()
    {
        if(Quests[0, 1] == "true" && Quests[1, 1] == "true")
        {
            return true;
        }
        return false;
    }
}
