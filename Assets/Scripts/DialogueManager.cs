using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> facts;
    
    void Start()
    {
        facts = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting converstaion");
        facts.Clear();

        foreach (string fact in dialogue.facts)
        {
            facts.Enqueue(fact);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (facts.Count == 0)
        {
            EndDialogue();
            return;
        }

        string fact = facts.Dequeue();
        Debug.Log(fact);
    }

    void EndDialogue()
    {
        Debug.Log("end of conversation");
    }
}
