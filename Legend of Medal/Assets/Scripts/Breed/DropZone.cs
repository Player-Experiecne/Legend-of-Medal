using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        BreedMachine.Instance.SelectDropZone(GetComponent<RectTransform>());
    }
}
