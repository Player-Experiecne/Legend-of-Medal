using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class BreedMachine : MonoBehaviour
{
    public static BreedMachine Instance;
    public Color highlightColor = Color.yellow;
    private Color defaultColor;
    private RectTransform highlightedDropZone;

    public TMP_Text resultText;
    private TissueItem selectedTissue;
    private RectTransform selectedDropZone;
    public BreedManager breedManager;
    public RectTransform[] dropZones;

    private Dictionary<RectTransform, TissueItem> tissuesInDropZones = new Dictionary<RectTransform, TissueItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 被DropZone调用来选择一个区域
    public void SelectDropZone(RectTransform dropZone)
    {
        // 如果之前有一个高亮的dropZone，恢复其默认颜色
        if (highlightedDropZone != null)
        {
            highlightedDropZone.GetComponent<Image>().color = defaultColor;
        }

        selectedDropZone = dropZone;

        // 保存当前dropZone的默认颜色
        defaultColor = dropZone.GetComponent<Image>().color;

        // 设置高亮颜色
        dropZone.GetComponent<Image>().color = highlightColor;

        // 记录当前被高亮的dropZone
        highlightedDropZone = dropZone;
    }


    // 被TissueItem调用来选择一个样本
    public void SelectTissueForBreeding(TissueItem tissue)
    {
        if (selectedDropZone != null)
        {
            // 调整tissue的尺寸以匹配dropZone的大小
            AdjustTissueSize(tissue, selectedDropZone);

            // 设置tissue的位置到dropZone上
            tissue.transform.position = selectedDropZone.position;

            // 在字典中记录该tissue被放置在该dropZone上
            tissuesInDropZones[selectedDropZone] = tissue;

            // 重置选中的dropZone
            selectedDropZone = null;
            selectedDropZone.GetComponent<Image>().color = defaultColor;
            highlightedDropZone = null; // 重置当前高亮的dropZone
        }
    }


    private void AdjustTissueSize(TissueItem tissue, RectTransform targetDropZone)
    {
        tissue.rectTransform.sizeDelta = targetDropZone.sizeDelta;
    }



    public void BreedTissuesOnClick()
    {
        if (tissuesInDropZones[dropZones[0]] != null && tissuesInDropZones[dropZones[1]] != null)
        {
            string offspring = breedManager.Breed(tissuesInDropZones[dropZones[0]].genotype, tissuesInDropZones[dropZones[1]].genotype);
            resultText.text = offspring;
            tissuesInDropZones[dropZones[0]] = null;
            tissuesInDropZones[dropZones[1]] = null;

            // 恢复高亮的dropZone的颜色
            if (highlightedDropZone != null)
            {
                highlightedDropZone.GetComponent<Image>().color = defaultColor;
                highlightedDropZone = null; // 重置当前高亮的dropZone
            }
        }
    }

}
