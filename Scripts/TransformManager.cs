using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{

    Vector3 deltaRight = new Vector3(0.2f, 0.0f, 0.0f);
    Vector3 deltaLeft = new Vector3(-0.2f, 0.0f, 0.0f);
    Vector3 deltaUp = new Vector3(0.0f, 0.2f, 0.0f);
    Vector3 deltaDown = new Vector3(0.0f, -0.2f, 0.0f);

    float rotSpeed = 200f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        //translate layer
        if (GameObject.FindGameObjectWithTag("selected"))
        {
            GameObject FocusObj = GameObject.FindGameObjectWithTag("selected");

            //do translate according to keyboard
            if (Input.GetKeyDown(KeyCode.A))
            {
                FocusObj.transform.position += deltaLeft;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                FocusObj.transform.position += deltaRight;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                FocusObj.transform.position += deltaUp;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                FocusObj.transform.position += deltaDown;
            }

            // do rotation according to keyboard
            if (Input.GetKeyDown(KeyCode.Z))
            {
                FocusObj.transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                FocusObj.transform.Rotate(-rotSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                FocusObj.transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                FocusObj.transform.Rotate(0, -rotSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                FocusObj.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                FocusObj.transform.Rotate(0, 0, -rotSpeed * Time.deltaTime);
            }

        }

        //translate root
        if (GameObject.FindGameObjectWithTag("activeRoot"))
        {
            //find frame of this root
            GameObject CurrentRoot = GameObject.FindGameObjectWithTag("activeRoot");
            //move root's frame
            Transform FrameT = CurrentRoot.transform.parent;
            //translate frame according to another set of key
            //because there would be selected layer and active root at the same time, using same set of control is difficult
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                FrameT.position += deltaLeft;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                FrameT.position += deltaRight;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                FrameT.position += deltaUp;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                FrameT.position += deltaDown;
            }
        }
    }

}
