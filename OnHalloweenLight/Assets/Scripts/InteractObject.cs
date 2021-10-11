using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour, IInteractable
{

    private float xDifference;
    private float yDifference;
    protected bool isLantern;
    protected SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        isLantern = false;
    }

    // Update is called once per frame
    void Update()
    {
        //InLanternRange();

    }

    public virtual void DoSomething()
    {

    }


    //i commented out the functionality of this method because it needs access to a lantern to do it and im trying to remove all the unneeded serialized fields
    //anyways this function and isLantern should probably be moved somewhere else where we handle lighting objects,theres probably going to be a light class or something and we can just 
    //access the lanterns via the global list in the game manager
    //public virtual void InLanternRange()
    //{
    //    if(!isLantern)
    //    {
    //        xDifference = lantern.transform.position.x - transform.position.x;
    //        yDifference = lantern.transform.position.y - transform.position.y;

    //        //Debug.Log(xDifference);
    //        //Debug.Log(yDifference);

    //        //Object displays full clarity if lantern really close
    //        if (xDifference > -1 && xDifference < 1) 
    //        {
    //            if (yDifference > -1 && yDifference < 1) 
    //            {
    //                sprite.color = Color.white;
    //            }
    //        }
    //        //object is a little dark if lantern farther away
    //        else if (xDifference > -4 && xDifference < 4) 
    //        {
    //            if (yDifference > -4 && yDifference < 4) 
    //            {
    //                sprite.color = Color.grey;
    //            }
    //        }
    //        //object is black if lantern far away
    //        else
    //        {
    //            sprite.color = Color.black;
    //        }
    //    }
    //}
}
