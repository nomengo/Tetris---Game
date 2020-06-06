using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject chosenGroup;
    //Groups
    public GameObject[] groups;
    private void Start()
    {
        //spawn initial group
        SpawnNext();
    }

    public void SpawnNext()
    {
        //random index
        int i = Random.Range(0, groups.Length);

        chosenGroup = groups[i];

        //random color numbers
        int r = Random.Range(0, 255);
        int g = Random.Range(0, 255);
        int b = Random.Range(0, 255);

        //spawn group at current position
        var group  = Instantiate(chosenGroup, transform.position, Quaternion.identity);

        foreach(var child in group.GetComponentsInChildren<SpriteRenderer>())
        {
            child.color = new Color(r/255f, g/ 255f, b/ 255f);
        }
    }
    
}
