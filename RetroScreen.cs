using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
public class RetroScreen : MonoBehaviour
{
    [Header("Shader Reference")]
    [Tooltip("Drag and drop the Hidden/PSX_PostProcess shader here")]
    public Shader psxPostProcessShader;

    [Header("Display Settings")]
    [Tooltip("Enable or disable the effect in Scene view")]
    public bool showInSceneView = true;

    [Header("PS1 Screen Resolution")]
    [Tooltip("Target pixel resolution (e.g., 320x240 or 256x224)")]
    public Vector2 targetResolution = new Vector2(320f, 240f);

    [Header("Color & Effect Settings")]
    [Range(4f, 32f)]
    [Tooltip("Color palette depth (Default for PS1 is 15-16)")]
    public float colorDepth = 15f;

    [Range(0f, 0.5f)]
    [Tooltip("Intensity of CRT scanlines")]
    public float scanlineIntensity = 0.12f;

    private Material psxMaterial;

    void OnEnable()
    {
        if (psxPostProcessShader == null)
        {
            psxPostProcessShader = Shader.Find("Hidden/PSX_PostProcess");
        }

        UpdateMaterial();
    }

    void OnValidate()
    {
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        if (psxPostProcessShader == null) return;

        if (psxMaterial == null || psxMaterial.shader != psxPostProcessShader)
        {
            psxMaterial = new Material(psxPostProcessShader);
            psxMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        psxMaterial.SetVector("_ScreenResolution", new Vector4(targetResolution.x, targetResolution.y, 0, 0));
        psxMaterial.SetFloat("_ColorDepth", colorDepth);
        psxMaterial.SetFloat("_ScanlineIntensity", scanlineIntensity);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Scene penceresinde çalışıp çalışmadığını kontrol eder
        bool isSceneCamera = Camera.current != null && Camera.current.cameraType == CameraType.SceneView;

        if (psxMaterial != null && (!isSceneCamera || showInSceneView))
        {
            Graphics.Blit(source, destination, psxMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    void OnDisable()
    {
        if (psxMaterial != null)
        {
            DestroyImmediate(psxMaterial);
        }
    }
}