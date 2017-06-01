using System.Collections;
using UnityEngine;


// allows to see changes without running the game
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostProcessDepthGrayscale : MonoBehaviour {

    public Material mat;

    private Camera cam; 

	void Start () {

        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
	}

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!mat) return; 

        Graphics.Blit(source,destination,mat);
    }
}
