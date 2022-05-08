using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

//Class for pause overlay mask
public class CutoutMask : Image
{
    //Material reverses effect of masking
    public override Material materialForRendering{
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
}

