using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject lantern;
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
        InLanternRange();

    }

    public virtual void DoSomething()
    {

    }

    public virtual void InLanternRange()
    {
        if(!isLantern)
        {
            xDifference = lantern.transform.position.x - transform.position.x;
            yDifference = lantern.transform.position.y - transform.position.y;

            //Debug.Log(xDifference);
            //Debug.Log(yDifference);

            //Object displays full clarity if lantern really close
            if (xDifference > -1 && xDifference < 1) 
            {
                if (yDifference > -1 && yDifference < 1) 
                {
                    sprite.color = Color.white;
                }
            }
            //object is a little dark if lantern farther away
            else if (xDifference > -4 && xDifference < 4) 
            {
                if (yDifference > -4 && yDifference < 4) 
                {
                    sprite.color = Color.grey;
                }
            }
            //object is black if lantern far away
            else
            {
                sprite.color = Color.black;
            }
        }
    }
}
