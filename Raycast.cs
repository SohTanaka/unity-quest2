using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    //Create private data type
    private GameObject Pointer;
    private LineRenderer laserRender;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        //Assign variables to pointer and laser
        Pointer = GameObject.Find("Pointer");
        laserRender = GameObject.Find("Laser").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Create ray

        //If Mouse moves... (this helps you test on play mode first)
        if (Input.GetAxis("Mouse X") != 0) {
            ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            GameObject.Find("CenterEyeAnchor").transform.position = new Vector3(0, 1, 0);
        } else { //This would be for VR
            ray = new Ray(this.transform.position, this.transform.forward);
        }

        //Get info of raycast
        RaycastHit hit;

        //Casts a ray in a direction, maxDistance
        if(Physics.Raycast(ray,out hit,100))
        {
            //show pointer
            Pointer.GetComponent<Renderer>().enabled = true;
            //re-position pointer on collision
            Pointer.transform.position = hit.point;
            //Extend laser distance to collision
            float laserDistance = Pointer.transform.position.z - GameObject.Find("RightHandAnchor").transform.position.z;
            laserRender.SetPosition(1, new Vector3(0, 0 ,laserDistance));
        } else {
            // hide pointer
            Pointer.GetComponent<Renderer>().enabled = false;
            //Reset the start point of laser to fixed distance
            laserRender.SetPosition(1, new Vector3(0, 0 ,0.5f));
        }

    }


}
