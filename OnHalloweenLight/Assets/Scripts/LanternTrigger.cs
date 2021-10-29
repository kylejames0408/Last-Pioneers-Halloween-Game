using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternTrigger : MonoBehaviour
{
    [SerializeField] protected LanternStand triggerStand1 = null;
    [SerializeField] protected LanternStand triggerStand2 = null;
    [SerializeField] protected LanternStand triggerStand3 = null;
    [SerializeField] protected LanternStand triggerStand4 = null;
    [SerializeField] protected bool questUpdate = false;


    /// <summary>
    /// true- all the connected stands need to have lanterns to trigger switch
    /// false- switch is triggered if any connected stands have a lantern
    /// </summary>
    public bool allStandsNeeded;



    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void doLanternTrigger()
    {
        
    }

    /// <summary>
    /// checks to see if the stands are triggered correctly
    /// </summary>
    /// <returns> if all the stands present are triggered</returns>
    public bool checkStands()
    {
        bool check = true;



        if(allStandsNeeded)
        {
            if (triggerStand1 != null)
            {
                if(!triggerStand1.hasLantern)
                {
                    check = false;
                }
            }
            if (triggerStand2 != null)
            {
                if (!triggerStand2.hasLantern)
                {
                    check = false;
                }
            }
            if (triggerStand3 != null)
            {
                if (!triggerStand3.hasLantern)
                {
                    check = false;
                }
            }
            if (triggerStand4 != null)
            {
                if (!triggerStand4.hasLantern)
                {
                    check = false;
                }
            }
            return check;
        }
        else
        {
            check = false;

            if (triggerStand1 != null)
            {
                if (triggerStand1.hasLantern)
                {
                    check = true;
                }
            }
            if (triggerStand2 != null)
            {
                if (triggerStand2.hasLantern)
                {
                    check = true;
                }
            }
            if (triggerStand3 != null)
            {
                if (triggerStand3.hasLantern)
                {
                    check = true;
                }
            }
            if (triggerStand4 != null)
            {
                if (triggerStand4.hasLantern)
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
