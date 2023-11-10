using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * updated 08.10.18
 * script to rotate a GameObject or world canvas to look at the user
 * place in object to rotate, or, in the object holding UI canvas or in actual canvas
*/

public class lookAtUser : MonoBehaviour {
	//variables
	[Tooltip ("Select the camera representing the user")]
	public string userTag = "MainCamera";
	[Tooltip ("Activate if you want the rotating Object to remain vertical")]
	public bool restrictY;

    private Transform user;

    // Update is called once per frame
    void Update()
    {
        if(user == null)
        {
            user = GameObject.FindGameObjectWithTag(userTag).transform;
        }
        Vector3 target; //create a temp Vector3 variable
                        /*
                            * Check the restrictY varaiable
                            * IF it is TRUE then set the temp target variable
                            * to the user position but use the Y position from the object this script is attached to
                            * IF it is FALSE the set the temp target variable to the user position
                            */
        if (restrictY)
        {
            target = new Vector3(user.position.x, this.transform.position.y, user.position.z);
        }
        else
        {
            target = user.transform.position;
        }
        transform.LookAt(target); //rotate the object this script is attached to towards the target position
    }

}
