using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour {

   

    public void AddToRoot()
    {

        GameObject FocusObj;
        GameObject CurrentRoot;
        // empty obj serves as parent for root and layers

        //find selected obj
        if (GameObject.FindGameObjectWithTag("selected"))
        {
            FocusObj = GameObject.FindGameObjectWithTag("selected");

            //find active root
            if (GameObject.FindGameObjectWithTag("activeRoot"))
            {
                CurrentRoot = GameObject.FindGameObjectWithTag("activeRoot");

                //if every root has a frame as parent         
                //add selected layer to this frame
                FocusObj.transform.parent = CurrentRoot.transform.parent;

            }

        }

    }

    //dissolve  a root-children relationship
    public void DissolveRoot()
    {
        GameObject CurrentRoot;

        //find active root
        if (GameObject.FindGameObjectWithTag("activeRoot"))
        {
            CurrentRoot = GameObject.FindGameObjectWithTag("activeRoot");
            //find its parent frame
            Transform FrameT = CurrentRoot.transform.parent;

            int i = 0;

            //find its children
            //every time can only find one???
            foreach (Transform childTransform in FrameT)
            {
     
                childTransform.parent = null;
                print(i);
                i++;
            }
            //but currentRoot itself still shall be frame's child
            CurrentRoot.transform.parent = FrameT;
        }

    }





    //remove a layer from root
    public void RemoveLayer()
    {

        GameObject FocusObj;

        //find selected obj
        if (GameObject.FindGameObjectWithTag("selected"))
        {
            FocusObj = GameObject.FindGameObjectWithTag("selected");

            //disable parent
            FocusObj.transform.parent = null;

        }
    }






}
