using UnityEngine;
using UnityEngine.UI;

public class AnalysisSystem : MonoBehaviour
{
    public static AnalysisSystem Instance;

    [SerializeField]
    private RectTransform analysisArea; // The UI area where tissues should be dragged for analysis.

    [SerializeField]
    private Text analysisResultText; // Display the genotype here.

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

    public bool IsOverAnalysisArea(Vector2 position)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(analysisArea, position);
    }

    public void AnalyzeTissue(Tissue tissue)
    {
        // Display the genotype of the tissue.
        analysisResultText.text = tissue.genotype;
    }
}
