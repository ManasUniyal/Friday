using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    
    // private static ScreenShot instance;
    public Camera myCamera;

    private int width;
    private int height;
    private bool take;

    // Start is called before the first frame update
    void Start()
    {
        // instance = this;
        // myCamera = gameObject.GetComponent<Camera>();
        // TakeScreenshotDefault();
    }

    // Update is called once per frame
    void OnPostRender()
    {
        if(take){
            take = false;

            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderRes = new Texture2D(renderTexture.width,renderTexture.height,TextureFormat.ARGB32,false);
            Rect rect = new Rect(0,0,renderTexture.width,renderTexture.height);
            renderRes.ReadPixels(rect,0,0);
            
            byte[] byteArray = renderRes.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Shot.png" , byteArray);

            Debug.Log("Saved");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;


        }
    }

    // public void TakeScreenshot(int width,int height){
    //     myCamera.targetTexture = RenderTexture.GetTemporary(width,height,16);
    //     take = true;
    // }

    // public static void TakeScreenshotStatic(int width,int height){
    //     instance.TakeScreenshot(width,height);
    // }

    public void TakeScreenshotDefault(){
        width = height = 200;
        myCamera.targetTexture = RenderTexture.GetTemporary(width,height,16);
        take = true;
    }
    

}
