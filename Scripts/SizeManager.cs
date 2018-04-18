using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SizeManager : MonoBehaviour
{
    //public sliders values
    public Slider InputSize;
    public Slider FilterNum;
    public Slider FilterSize;
    public Slider Stride;

    public InputField InputS;
    public InputField FilterN;
    public InputField FilterS;
    public InputField Sd;

    // public text on panel
    public Text LayerInput;
    public Text LayerSize;
    public Text LayerFilter;
    public Text LayerStride;

    GameObject FocusObj;

    // size manager
    //var for update
    float rate = 0.001f;
    float filterNum = 64f; //must be float, reason see public funcs
    float layerSize = 224f;


    //var default size for sliders
    //keep this default, inputLayer's size is correct
    float inputTensorSize = 224f;
    float filterSize = 1f;
    float stride = 1f;


    public void ChangeSize()
    {
        //only update size when button pressed to update!!!

        // find the selected obj
        if (GameObject.FindGameObjectWithTag("selected"))
        {


            FocusObj = GameObject.FindGameObjectWithTag("selected");

            //since every layer when been created, will be "selected"
            //later, whenever been selected, their size been reset.

            // every para can be changed in funcs below, controled by sliders/input- outlet
            //compare slider+outlet
            //if inputfiled has valid value, use it; if not, use slider

            //input
            if (InputS.text != "")
            {
                inputTensorSize = float.Parse(InputS.text);
                //check for boundry, if illegal, back to default size
                inputTensorSize = (inputTensorSize >= 32f && inputTensorSize <= 1024f) ? inputTensorSize : 224f;
            }
            else
            {
                inputTensorSize = InputSize.value;
            }

            //filter number
            if (FilterN.text != "")
            {
                filterNum = float.Parse(FilterN.text);
                //check for boundry, if illegal, back to default size
                filterNum = (filterNum >= 3f && filterNum <= 1024f) ? filterNum : 64f;
            }
            else
            {
                filterNum = FilterNum.value;
            }

            //filter size
            if (FilterS.text != "")
            {
                filterSize = float.Parse(FilterS.text);
                //check for boundry, if illegal, back to default size
                filterSize = (filterSize >= 1f && filterSize <= 7f) ? filterSize : 1f;
            }
            else
            {
                filterSize = FilterSize.value;
            }

            //stride
            if (Sd.text != "")
            {
                stride = float.Parse(Sd.text);
                //check for boundry, if illegal, back to default size
                stride = (stride >= 1f && stride <= 3f) ? stride : 1f;
            }
            else
            {
                stride = Stride.value;
            }

            
            //calculate the final
            layerSize = (inputTensorSize - filterSize) / stride + 1; //output layersize

            layerSize = (float)Mathf.Floor(layerSize);

            // 224x224  = 0.224 in scale, means rate = 0.001
            //localScale widen obj
            //calculate changed obj in vector: x, z is the layer size; y is filter number
            float x = layerSize * rate;
            float y = filterNum * rate;

            FocusObj.transform.localScale = new Vector3(x, y, x);


            //store parameters in class layer
            Layer layerInfo = FocusObj.GetComponent<Layer>();

            //updating class layer's value
            layerInfo.Input = inputTensorSize.ToString() + "x" + inputTensorSize.ToString(); //input's thickness does not matter here, since you only compare its output thicknes to other inputlayer's
            layerInfo.Size = layerSize.ToString() + "x" + layerSize.ToString() + "x" + filterNum.ToString();
            layerInfo.Filter = filterSize.ToString() + "x" + filterSize.ToString();
            layerInfo.Stride = stride.ToString();

            //but for some layers, only part of the info is valid info and should be shown
            //get layer name, decide which info to show depends on name
            //Conv and Pooling show all those info
            //Input and Dense only show name and size
            if ((layerInfo.Name == "Input") || (layerInfo.Name == "Dense")|| (layerInfo.Name == "IdentityBlock"))
            {
                layerInfo.Filter = "";
                layerInfo.Stride = "";
            }

            //block only show name
            if (layerInfo.Name == "IdentityBlock")
            {
                layerInfo.Size = "";
            }

                //update panel
                LayerInput.text = layerInfo.Input;
            LayerSize.text = layerInfo.Size;
            LayerFilter.text = layerInfo.Filter;
            LayerStride.text = layerInfo.Stride;


        }


    }

    /*
    //in separated func : add input_slide's gameobj to size manager, func to this one
    public void ChangeSizeByInput(float newInputTensorSize) //must use float, so that the func would appear in "public dynamic parameters" on slide
    {

        inputTensorSize = newInputTensorSize;


    }

    //----------------------------------------------------------------------
    //add filterSize_slide's gameobj to size manager, func to this one
    public void ChangeSizeByFilter(float newFilterSize)
    {

        filterSize = newFilterSize;

    }

    //----------------------------------------------------------------------
    //add stride_slide's gameobj to size manager, func to this one
    public void ChangeSizeByStride(float newStride)
    {

        stride = newStride;

    }

    //----------------------------------------------------------------------
    //add filterNum_slide's gameobj to size manager, func to this one
    public void ChangeSizeByFilterNum(float newFilterNum)
    {

        filterNum = newFilterNum;

    }

    //--------------------------------------------------------------------------------------------------
    //in separated func : add input_field's gameobj to size manager, func to this one
    public void ChangeSizeByInput(string newInputTensorSize) //must use float, so that the func would appear in "public dynamic parameters" on slide
    {

        inputTensorSize = float.Parse(newInputTensorSize);
        //check for boundry, if illegal, back to default size
        inputTensorSize = (inputTensorSize >= 32f && inputTensorSize <= 1024f) ? inputTensorSize : 224f;


    }

    //----------------------------------------------------------------------
    //add filterSize_slide's gameobj to size manager, func to this one
    public void ChangeSizeByFilter(string newFilterSize)
    {

        filterSize = float.Parse(newFilterSize);
        //check for boundry, if illegal, back to default size
        filterSize = (filterSize >= 1f && filterSize <= 7f) ? filterSize : 1f;

    }

    //----------------------------------------------------------------------
    //add stride_slide's gameobj to size manager, func to this one
    public void ChangeSizeByStride(string newStride)
    {

        stride = float.Parse(newStride);
        //check for boundry, if illegal, back to default size
        stride = (stride >= 1f && stride <= 3f) ? stride : 1f;

    }

    //----------------------------------------------------------------------
    //add filterNum_slide's gameobj to size manager, func to this one
    public void ChangeSizeByFilterNum(string newFilterNum)
    {

        filterNum = float.Parse(newFilterNum);
        //check for boundry, if illegal, back to default size
        filterNum = (filterNum >= 3f && filterNum <= 1024f) ? filterNum : 64f;

    }
    
    //change entire model's scale according to this sliders
    public void ChangeScale(float step)
    {
        GameObject CurrentRoot;
        Vector3 deltaAway = new Vector3(0.0f, 0.0f, -0.1f);

        //find the active root
        if (GameObject.FindGameObjectWithTag("activeRoot"))
        {

            CurrentRoot = GameObject.FindGameObjectWithTag("activeRoot");
            //move root's frame
            Transform FrameT = CurrentRoot.transform.parent;

            //translate it on z-axis
            deltaAway.z *= step;
            FrameT.position += deltaAway;

        }



    }
    */

}
