using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> facts;
    public TMP_Text dialogueText;
    public GameObject textBoxUI;
    public Animator animator;
    
    void Start()
    {
        facts = new Queue<string>();
        textBoxUI.SetActive(false);
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
        animator.SetBool("isOpen", true);
        facts.Clear();
        textBoxUI.SetActive(true);

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
        StopAllCoroutines();
        StartCoroutine(SentenceScroll(fact));
    }

    IEnumerator SentenceScroll(string fact)
    {
        dialogueText.text = "";

        foreach (char letter in fact.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        // textBoxUI.SetActive(false);
    }
}
