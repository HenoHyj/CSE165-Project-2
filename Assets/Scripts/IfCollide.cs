using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLIDING!!! Collide with: " + collision.collider.name);
        if(collision.collider.name == "machu_picchu_2")
            GlobalVariables.crashed = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
