using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/ProjectionCorrection")]
public class ProjectionCorrection : MonoBehaviour
{
    [HideInInspector]
    public Shader HorizontalFullShader;
    [HideInInspector]
    public Shader VerticalFullShader;
    [HideInInspector]
    public Shader HorizontalSimpleShader;
    [HideInInspector]
    public Shader VerticalSimpleShader;
    private Shader shader;

    public bool horizontal = true;
    public bool simple = false;

    public float Intensity = 1; //0-1

    private Material _material;

    private void Awake()
    {
        InitShader();
        _material = new Material(shader);
    }

    public void InitShader()
    {
        if (horizontal)
        {
            if (simple)
            {
                shader = HorizontalSimpleShader;
            }
            else
            {
                shader = HorizontalFullShader;
            }
        }
        else
        {
            if (simple)
            {
                shader = VerticalSimpleShader;
            }
            else
            {
                shader = VerticalFullShader;
            }
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Shader oldShader = shader;
        InitShader();
        if (shader != oldShader)
        {
            _material = new Material(shader);
        }

        Intensity = Mathf.Clamp01(Intensity);
        float depth;
        depth = Screen.height / (2 * Mathf.Tan(camera.fov * Mathf.Deg2Rad * 0.5f));
        _material.SetFloat("_Depth", depth);
        if (horizontal)
        {
            _material.SetFloat("_Width", Screen.width);
        }
        else
        {
            _material.SetFloat("_Width", Screen.height);
        }
        _material.SetFloat("_Intensity", Intensity);
        Graphics.Blit(source, destination, _material);
    }
}
