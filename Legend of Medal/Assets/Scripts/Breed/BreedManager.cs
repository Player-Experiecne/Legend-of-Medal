using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedManager : MonoBehaviour
{
    public string Breed(string parent1, string parent2)
    {
        if (parent1.Length != parent2.Length)
        {
            Debug.LogError("Parent genes are not of the same length!");
            return null;
        }

        char[] offspringGenes = new char[parent1.Length];

        for (int i = 0; i < parent1.Length; i += 2)
        {
            char geneFromParent1 = GetRandomGeneFromPair(parent1[i], parent1[i + 1]);
            char geneFromParent2 = GetRandomGeneFromPair(parent2[i], parent2[i + 1]);

            if (char.IsUpper(geneFromParent1) || char.IsUpper(geneFromParent2))
            {
                offspringGenes[i] = char.ToUpper(geneFromParent1);
                offspringGenes[i + 1] = char.ToUpper(geneFromParent2);
            }
            else
            {
                offspringGenes[i] = char.ToLower(geneFromParent1);
                offspringGenes[i + 1] = char.ToLower(geneFromParent2);
            }
        }

        return new string(offspringGenes);
    }

    private char GetRandomGeneFromPair(char gene1, char gene2)
    {
        return UnityEngine.Random.value > 0.5f ? gene1 : gene2;
    }
}

