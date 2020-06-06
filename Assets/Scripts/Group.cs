using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public static bool isGameOver;
    //time since last gravity tick
    float lastFall = 0;
    void Start()
    {
        isGameOver = false;
        //default position not valid then game is over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER !!!");
            Destroy(gameObject);
            isGameOver = true;
        }
    }

    void Update()
    {
        //move left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //modify position
            transform.position += new Vector3(-1,0,0);

            //see if it's valid
            if (isValidGridPos())
            {
                //it's valid , update grid
                updateGrid();
            }
            else
            {
                //it's not valid , revert
                transform.position += new Vector3(1, 0, 0);
            }
        }
        //move to the right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //modify position
            transform.position += new Vector3(1, 0, 0);

            //see if it's valid
            if (isValidGridPos())
            {
                //it's valid , update grid
                updateGrid();
            }
            else
            {
                //it's not valid , revert
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        //rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // see if valid
            if (isValidGridPos())
            {
                // it's valid. Update grid.
                updateGrid();
            }
            else
            {
                // it's not valid. revert.
                transform.Rotate(0, 0, 90);
            }
        }
        //move downwards and fall
        else if (Input.GetKeyDown(KeyCode.DownArrow)||Time.time - lastFall >= 1)
        {
            //modify position
            transform.position += new Vector3(0, -1, 0);

            //see if valid
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                //clear filled horizontal lines
                Playfield.deleteFullRows();

                //spawn next group
                FindObjectOfType<Spawner>().SpawnNext();

                //disable script
                enabled = false;

            }
            lastFall = Time.time;
        }
    }

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            //check if not inside border
            if (!Playfield.insideBorder(v))
            {
                return false;
            }

            //block in grid cell
            if(Playfield.grid[(int)v.x,(int)v.y]!=null && Playfield.grid[(int)v.x,(int)v.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }
    void updateGrid()
    {
        //remove old children from grid
        for (int y = 0; y < Playfield.H; ++y)
        {
            for (int x = 0; x < Playfield.W; ++x)
            {
                if (Playfield.grid[x, y] != null)
                {
                    if(Playfield.grid[x,y].parent == transform)
                    {
                        Playfield.grid[x, y] = null;
                    }
                }
            }
        }
        //add new children to grid 
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
