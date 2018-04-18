using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{


    public GameObject ConvLayer;
   
    public GameObject InputLayer;
    public GameObject PoolingLayer;
    public GameObject DenseLayer;
    public GameObject FrameModel;
    public GameObject FrameBlock;
    public GameObject BlockInModel;



    // public text on panel
    public Text LayerName;
    public Text LayerInput;
    public Text LayerSize;
    public Text LayerFilter;
    public Text LayerStride;

    public Text CurrentRoot;
   
    GameObject oldFocusedObj;
    GameObject NewLayerOut;
    GameObject NewLayerIn;

    const int DefaultLayerSize = 224;
    const int DefaultFilterNum = 64;
    float rate = 0.001f;


    public void CreateConvLayer() //all conv layer consist of two parts: parent: conv, and child: input -> this child cannot be selected, no info
    {
        // unselected the current selected layer
        if (GameObject.FindGameObjectWithTag("selected"))
        {
            oldFocusedObj = GameObject.FindGameObjectWithTag("selected");
            oldFocusedObj.tag = "unselected";
        }

        Instantiate(ConvLayer);
        ConvLayer.tag = "selected";
       

        // set to default size
        float x = DefaultLayerSize * rate;
        float y = DefaultFilterNum * rate;

        ConvLayer.transform.localScale = new Vector3(x, y, x);


        //store parameters in class layer
        Layer layerInfo = ConvLayer.GetComponent<Layer>();

        //set parameters value of the layer 
        layerInfo.Name = "Conv";
        layerInfo.Input = "224x224";
        layerInfo.Size = "224x224x64";
        layerInfo.Filter = "1x1";
        layerInfo.Stride = "1";


        //show parameter on the panel's text
        LayerName.text = layerInfo.Name;
        LayerInput.text = layerInfo.Input;
        LayerSize.text = layerInfo.Size;
        LayerFilter.text = layerInfo.Filter;
        LayerStride.text = layerInfo.Stride;



    }
    

    public void CreateInpuLayer()
    {
        // unselected the current selected layer
        if (GameObject.FindGameObjectWithTag("selected"))
        {
            oldFocusedObj = GameObject.FindGameObjectWithTag("selected");
            oldFocusedObj.tag = "unselected";
        }

        Instantiate(InputLayer);
        InputLayer.tag = "selected";

        float x = DefaultLayerSize * rate;
        float y = DefaultFilterNum * rate;
        InputLayer.transform.localScale = new Vector3(x, y, x);

        //store parameters in class layer
        Layer layerInfo = InputLayer.GetComponent<Layer>();

        //set parameters value of the layer 
        layerInfo.Name = "Input";
        layerInfo.Size = "224x224";


        //show parameter on the panel's text
        LayerName.text = layerInfo.Name;
        LayerSize.text = layerInfo.Size;
        LayerInput.text = "";
        LayerFilter.text = "";
        LayerStride.text = "";





    }


    public void CreatePoolingLayer()
   {
       // unselected the current selected layer
       if (GameObject.FindGameObjectWithTag("selected"))
       {
           oldFocusedObj = GameObject.FindGameObjectWithTag("selected");
           oldFocusedObj.tag = "unselected";
       }

       Instantiate(PoolingLayer);
       PoolingLayer.tag = "selected";

       float x = DefaultLayerSize / 2 * rate; //(224-2)/2+1 ==224/2
       float y = DefaultFilterNum / 2 * rate;
       PoolingLayer.transform.localScale = new Vector3(x, y, x);


       //store parameters in class layer
       Layer layerInfo = PoolingLayer.GetComponent<Layer>();

       //set parameters value of the layer 
       layerInfo.Name = "Pooling";
       layerInfo.Input = "224x224";
       layerInfo.Size = "112x112x64";
       layerInfo.Filter = "2x2";
       layerInfo.Stride = "2";


        //show parameter on the panel's text
        LayerName.text = layerInfo.Name;
       LayerInput.text = layerInfo.Input;
       LayerSize.text = layerInfo.Size;
       LayerFilter.text = layerInfo.Filter;
       LayerStride.text = layerInfo.Stride;


   }


   public void CreateDenseLayer()
   {
       // unselected the current selected layer
       if (GameObject.FindGameObjectWithTag("selected"))
       {
           oldFocusedObj = GameObject.FindGameObjectWithTag("selected");
           oldFocusedObj.tag = "unselected";
       }

       Instantiate(DenseLayer);
       DenseLayer.tag = "selected";

       float x = DefaultFilterNum * rate;
       float y = DefaultFilterNum * rate; //if give it 1, you cannot see it
       DenseLayer.transform.localScale = new Vector3(x, y, x);

       //store parameters in class layer
       Layer layerInfo = DenseLayer.GetComponent<Layer>();

       //set parameters value of the layer 
       layerInfo.Name = "Dense";



        //show parameter on the panel's text
       LayerName.text = layerInfo.Name;
       LayerSize.text = layerInfo.Size;
        LayerInput.text = "";
        LayerFilter.text = "";
        LayerStride.text = "";




    }
    public void CreateBlockInModel()
    {
        // unselected the current selected layer
        if (GameObject.FindGameObjectWithTag("selected"))
        {
            oldFocusedObj = GameObject.FindGameObjectWithTag("selected");
            oldFocusedObj.tag = "unselected";
        }

        Instantiate(BlockInModel); //the sphere
                                 //block is its (first) child
        GameObject Block = FrameBlock.transform.GetChild(0).gameObject;
        BlockInModel.tag = "selected";

        float x = DefaultFilterNum * rate;
        float y = DefaultFilterNum * rate; //if give it 1, you cannot see it
        BlockInModel.transform.localScale = new Vector3(x, y, x);


        //store parameters in class layer
        Layer layerInfo = BlockInModel.GetComponent<Layer>();

        //set parameters value of the layer 
        layerInfo.Name = "IdentityBlock";


        //show parameter on the panel's text
        LayerName.text = layerInfo.Name;
        LayerSize.text = "";
        LayerInput.text = "";
        LayerFilter.text = "";
        LayerStride.text = "";


    }



    // create root of model
    public void CreateModel()
   {
       // unselected the current active root
       if (GameObject.FindGameObjectWithTag("activeRoot"))
       {
           oldFocusedObj = GameObject.FindGameObjectWithTag("activeRoot");
           oldFocusedObj.tag = "inactiveRoot";
       }

       Instantiate(FrameModel); //the sphere

      //model is its (first) child
       GameObject Model = FrameModel.transform.GetChild(0).gameObject;
       Model.tag = "activeRoot";
       Model.layer = 8; // index for "root"



        float x = DefaultFilterNum * rate; // what is a sphere's parameter?
       float y = DefaultFilterNum * rate;
       Model.transform.localScale = new Vector3(x, y, x);


        //store parameters in class layer
        Layer RootInfo = Model.GetComponent<Layer>();

       //set parameters value of the layer 
       RootInfo.Name = "Model";

        //show parameter on the panel's text
        CurrentRoot.text = RootInfo.Name;


   }

   //create root of block
   public void CreateBlock()
   {
       // unselected the current active root
       if (GameObject.FindGameObjectWithTag("activeRoot"))
       {
           oldFocusedObj = GameObject.FindGameObjectWithTag("activeRoot");
           oldFocusedObj.tag = "inactiveRoot";
       }

       Instantiate(FrameBlock); //the sphere
         //block is its (first) child
       GameObject Block = FrameBlock.transform.GetChild(0).gameObject;
       Block.tag = "activeRoot";
       Block.layer = 8; // index for "root"

        float x = 0.02f;
       Block.transform.localScale = new Vector3(x, x, x);

       //store parameters in class layer
       Layer RootInfo = Block.GetComponent<Layer>();

       //set parameters value of the layer 
       RootInfo.Name = "IdentityBlock";

        //show parameter on the panel's text
       CurrentRoot.text = RootInfo.Name;

   }
   
  
}
