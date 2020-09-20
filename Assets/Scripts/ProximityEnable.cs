using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEnable : MonoBehaviour
{
    public List<GameObject> listOfChildren;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GetChildRecursive(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(GameObject obj in listOfChildren)
        {
            if(obj != null) { 
                if(Vector2.Distance(obj.transform.position, player.position) > 30)
                {
                    obj.SetActive(false);
                
                }
                else
                {
                obj.SetActive(true);
                }
            }
        }
    }

    //Found online at https://stackoverflow.com/questions/37943729/get-all-children-children-of-children-in-unity3d
    private void GetChildRecursive(GameObject obj)  
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            listOfChildren.Add(child.gameObject);
            GetChildRecursive(child.gameObject);
        }
    }
}
