using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : InteractObject
{

    public static int crowCount = 0;

    float offset;
    int direction;


    // Start is called before the first frame update
    void Start()
    {
        crowCount++;
        offset = 0;
        if (Random.Range(0.0f, 1.0f) < .5)
        {
            direction = -1;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else { direction = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameState)
        {

            case GameState.Game:
                if(offset == 0)
                {
                    if(this.touchingLight)
                    {
                        offset = 1;
                        offset++;
                        crowCount--;
                    }
                }
                else
                {
                    this.transform.Translate(new Vector2(direction*(offset)/20,(offset*offset)/20));
                }


                if(this.transform.position.y>100)
                {
                    this.enabled = false;
                }
                break;

        }
    }
}
