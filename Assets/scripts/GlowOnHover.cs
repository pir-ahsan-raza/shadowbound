using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GlowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    private Material mat;
    private float defaultGlow;
    private Color defaultColor;

    void Start()
    {
        if (text == null)
            text = GetComponentInChildren<TextMeshProUGUI>();

        mat = text.fontSharedMaterial;
        defaultGlow = mat.GetFloat(ShaderUtilities.ID_GlowPower);
        defaultColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mat.SetFloat(ShaderUtilities.ID_GlowPower, 0.7f);
        text.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mat.SetFloat(ShaderUtilities.ID_GlowPower, defaultGlow);
        text.color = defaultColor;
    }
}
