using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonologueData", menuName = "Data/Monologue", order = 1)]
public class MonologueData : ScriptableObject
{
    public float timeBetweenEachSentence = 2f;
    public List<string> sentences = new List<string>();

    public string GetSentence(int index)
    {
        return sentences[index];
    }

    public int GetNumberOfSentences()
    {
        return sentences.Count;
    }
}
