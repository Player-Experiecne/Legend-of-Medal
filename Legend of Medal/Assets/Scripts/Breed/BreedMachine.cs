using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BreedMachine : MonoBehaviour
{
    public static BreedMachine Instance;
    

    public TMP_Text resultText;
    private TissueItem[] tissuesToBreed = new TissueItem[2];
    public BreedManager breedManager;
    public RectTransform[] dropZones;

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

    public bool IsOverMachineArea(Vector2 position)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), position);
    }

    public void AddTissueForBreeding(TissueItem tissue)
    {
        if (tissuesToBreed[0] == null)
        {
            tissuesToBreed[0] = tissue;
        }
        else if (tissuesToBreed[1] == null)
        {
            tissuesToBreed[1] = tissue;

            // Once both tissues are added, breed them
            BreedTissues();
        }
    }

    private void BreedTissues()
    {
        string offspring = breedManager.Breed(tissuesToBreed[0].genotype, tissuesToBreed[1].genotype);
        resultText.text = offspring;

        // Optionally reset for next breeding
        tissuesToBreed[0] = null;
        tissuesToBreed[1] = null;
    }
}
