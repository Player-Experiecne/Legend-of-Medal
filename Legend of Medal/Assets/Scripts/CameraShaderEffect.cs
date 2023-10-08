using UnityEngine;

[ExecuteInEditMode]
public class CameraShaderEffect : MonoBehaviour
{
    public Material shaderMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shaderMaterial != null)
        {
            Graphics.Blit(source, destination, shaderMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
