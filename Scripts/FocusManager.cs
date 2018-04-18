using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusManager : MonoBehaviour
{

    // var needed for find game object
    GameObject oldFocusedObj;

    //var needed for newly selected by mouse obj 
    GameObject FocusedObject;

    // public text on panel
    public Text LayerName;
    public Text LayerInput;
    public Text LayerSize;
    public Text LayerFilter;
    public Text LayerStride;

    public Text CurrentRoot;


    private void Update()
    {
        RaycastHit hitInfo;

        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            FocusedObject = hitInfo.collider.gameObject;
            //if hit an obj on root layer
            if (FocusedObject.layer == 8) //layer8: root
            {
                oldFocusedObj = GameObject.FindGameObjectWithTag("activeRoot");
                oldFocusedObj.tag = "inactiveRoot";

                FocusedObject.tag = "activeRoot";

                //update panel
                //store parameters in class layer
                Layer RootInfo = FocusedObject.GetComponent<Layer>();
                CurrentRoot.text = RootInfo.Name;
            }
            else
            {//else, just hit an obj/layer
             //change current slected->unselected
                oldFocusedObj = GameObject.FindGameObjectWithTag("selected");
                oldFocusedObj.tag = "unselected";
                // change this one to be selected           
                FocusedObject.tag = "selected";

                //store parameters in class layer
                Layer layerInfo = FocusedObject.GetComponent<Layer>();

                //update the panel
                LayerName.text = layerInfo.Name;
                LayerInput.text = layerInfo.Input;
                LayerSize.text = layerInfo.Size;
                LayerFilter.text = layerInfo.Filter;
                LayerStride.text = layerInfo.Stride;
            }
        }
    }


}
