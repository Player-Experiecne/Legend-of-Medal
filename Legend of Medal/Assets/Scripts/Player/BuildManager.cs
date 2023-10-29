using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject defenderPrefab;

    public int maxDefenders = 5; // Maximum number of defenders allowed at the start
    private int currentDefenders = 0; // Current number of defenders placed

    // Extra defenders added after each wave
    public int extraDefendersPerWave = 2;

    public TextMeshProUGUI defenderCountText; // Reference to the TextMeshPro component

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PlaceDefender(hit.point);
            }
        }
    }

    void PlaceDefender(Vector3 position)
    {
        // Check if the player can still place defenders
        if (currentDefenders < maxDefenders)
        {
            Instantiate(defenderPrefab, position, Quaternion.identity);
            currentDefenders++; // Increase the current count of defenders
            UpdateDefenderText(); // Update the TMP text
        }
        else
        {
            Debug.Log("Defender limit reached!");
        }
    }

    // Call this method when a wave is completed
    public void OnWaveCompleted()
    {
        maxDefenders += extraDefendersPerWave;
        Debug.Log($"Extra defenders added! New limit: {maxDefenders}");
        UpdateDefenderText(); // Update the TMP text
    }

    public void OnLevelCompleted()
    {
        currentDefenders = 0;
        Debug.Log($"CurrentDefenders is set to 0");
        UpdateDefenderText(); // Update the TMP text
    }

    void UpdateDefenderText()
    {
        int defendersLeft = maxDefenders - currentDefenders;
        defenderCountText.text = $"Max Defenders: {maxDefenders}\nDefenders Left: {defendersLeft}";
    }
}
