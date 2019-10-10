using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonologueManager : MonoBehaviour
{
    [SerializeField]
    GameObject textCanvas;
    [SerializeField]
    Text monologueText;

    [SerializeField]
    MonologueData curData;
    [SerializeField]
    Queue<MonologueData> monologueList = new Queue<MonologueData>();

    float curSentenceTime;
    int curSentenceIndex = 0;
    bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            monologueText.text = curData.GetSentence(curSentenceIndex);
            curSentenceTime -= Time.deltaTime;
            if (IsCurrentSentenceFinished())
            {
                if (IsMonologueFinished())
                {
                    Reset();
                    if (IsMonologueInQueue())
                    {
                        PlayNext();
                    }
                }
                else
                {
                    NextSentence();
                }
            }
        }
        else
        {
            textCanvas.SetActive(false);
        }
    }
    private void PlayNext()
    {
        Play(monologueList.Dequeue());
    }

    private bool IsMonologueInQueue()
    {
        return monologueList.Count > 0;
    }

    private bool IsCurrentSentenceFinished()
    {
        return curSentenceTime <= 0;
    }

    private void NextSentence()
    {
        curSentenceIndex++;
        curSentenceTime = curData.timeBetweenEachSentence;
    }

    private bool IsMonologueFinished()
    {
        return curSentenceIndex + 1 >= curData.GetNumberOfSentences();
    }

    private void Reset()
    {
        isPlaying = false;
        curData = null;
        curSentenceIndex = 0;
        curSentenceTime = 0;
        monologueText.text = "";
        textCanvas.SetActive(false);
    }
    public void QueueMonologue(MonologueData newMonologue)
    {
        monologueList.Enqueue(newMonologue);
        if (!isPlaying)
        {
            Reset();
            PlayNext();
        }
    }
    public void Play(MonologueData monologue)
    {
        isPlaying = true;
        curSentenceTime = monologue.timeBetweenEachSentence;
        curData = monologue;
        textCanvas.SetActive(true);
    }
    public void Play(string sentence)
    {
        var monologue = new MonologueData();
        monologue.sentences = new List<string>();
        monologue.sentences.Add(sentence);
        monologue.timeBetweenEachSentence = 2;
        isPlaying = true;
        curSentenceTime = monologue.timeBetweenEachSentence;
        curData = monologue;
        textCanvas.SetActive(true);
    }
    public bool IsPlaying()
    {
        return isPlaying;
    }
}
