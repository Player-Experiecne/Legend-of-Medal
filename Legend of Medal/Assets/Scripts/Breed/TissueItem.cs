using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TissueItem : MonoBehaviour, IPointerClickHandler
{
    public string genotype;
    public BreedMachine breedMachine;
    public Image tissueImage;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private Color normalColor = Color.white; // 正常颜色
    private Color selectedColor = Color.yellow; // 选中时的颜色

    private Image image; // Reference to the Image component

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        breedMachine.SelectTissueForBreeding(this);
        Highlight();
    }

    public void Highlight()
    {
        if (image != null)
        {
            image.color = selectedColor;
        }
    }

    public void ResetHighlight()
    {
        if (image != null)
        {
            image.color = normalColor;
        }
    }
}
