using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class SpriteHoverOutline : MonoBehaviour
{
    [Header("Outline Settings")]
    [SerializeField]
    private Color outlineColor = Color.white;

    [SerializeField]
    [Range(0f, 10f)]
    private float outlineWidth = 10f;

    [SerializeField]
    [Range(0f, 1f)]
    private float alphaThreshold = 0.51f;


    [Header("Initial State")]
    [SerializeField]
    private bool outlineEnabled = false;


    private SpriteRenderer spriteRenderer;

    private MaterialPropertyBlock propertyBlock;


    // Shader property IDs
    private static readonly int OutlineColor =
        Shader.PropertyToID("_OutlineColor");

    private static readonly int OutlineWidth =
        Shader.PropertyToID("_OutlineWidth");

    private static readonly int OutlineEnabled =
        Shader.PropertyToID("_OutlineEnabled");

    private static readonly int AlphaThreshold =
        Shader.PropertyToID("_AlphaThreshold");


    public Material materialOutline;

    public bool hoverOutline = true;

    private void Awake()
    {
        if (materialOutline == null)
        {
            materialOutline = Resources.Load<Material>("SpriteHoverOutline_Material");
        }

        spriteRenderer =
            GetComponent<SpriteRenderer>();

        propertyBlock =
            new MaterialPropertyBlock();

        ApplyShaderProperties();
    }

    private void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();
        sr.material = materialOutline;
        BoxCollider2D cl = gameObject.GetComponent<BoxCollider2D>() ?? gameObject.AddComponent<BoxCollider2D>();
        cl.isTrigger = true;
        cl.size = sr.sprite.bounds.size;

    }

    private void OnMouseEnter()
    {
        SetOutline(true);
    }


    private void OnMouseExit()
    {
        SetOutline(false);
    }


    private void SetOutline(bool enabled)
    {
        if (hoverOutline == true)
        {
            outlineEnabled = enabled;

            ApplyShaderProperties();
        }

    }


    private void ApplyShaderProperties()
    {
        if (spriteRenderer == null)
            return;


        // Ambil PropertyBlock yang sedang digunakan
        spriteRenderer.GetPropertyBlock(
            propertyBlock
        );


        // Outline Color
        propertyBlock.SetColor(
            OutlineColor,
            outlineColor
        );


        // Outline Width
        propertyBlock.SetFloat(
            OutlineWidth,
            outlineWidth
        );


        // Alpha Threshold
        propertyBlock.SetFloat(
            AlphaThreshold,
            alphaThreshold
        );


        // Outline Enabled
        propertyBlock.SetFloat(
            OutlineEnabled,
            outlineEnabled ? 1f : 0f
        );


        // Terapkan PropertyBlock
        spriteRenderer.SetPropertyBlock(
            propertyBlock
        );
    }


    // =========================================================
    // PUBLIC METHODS
    // Bisa dipanggil oleh script lain
    // =========================================================

    public void ShowOutline()
    {
        SetOutline(true);
    }


    public void HideOutline()
    {
        SetOutline(false);
    }


    public void SetOutlineColor(Color color)
    {
        outlineColor = color;

        ApplyShaderProperties();
    }


    public void SetOutlineWidth(float width)
    {
        outlineWidth =
            Mathf.Clamp(
                width,
                0f,
                10f
            );

        ApplyShaderProperties();
    }


    public void SetAlphaThreshold(float threshold)
    {
        alphaThreshold =
            Mathf.Clamp01(
                threshold
            );

        ApplyShaderProperties();
    }


    // =========================================================
    // OPTIONAL
    // Untuk mengambil status outline
    // =========================================================

    public bool IsOutlineEnabled()
    {
        return outlineEnabled;
    }
}