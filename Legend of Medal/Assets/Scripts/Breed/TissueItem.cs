using UnityEngine;
using UnityEngine.EventSystems;

public class TissueItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public string genotype;

    private Vector3 originalPosition;

    public BreedMachine breedMachine;


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool isInDropZone = false;

        foreach (RectTransform dropZone in breedMachine.dropZones)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(dropZone, Input.mousePosition, eventData.pressEventCamera))
            {
                // 鼠标释放在这个dropZone上
                this.transform.SetParent(dropZone);
                this.transform.position = dropZone.position;
                isInDropZone = true;
                break;
            }
        }

        if (!isInDropZone)
        {
            transform.position = originalPosition;
        }
    }


}
