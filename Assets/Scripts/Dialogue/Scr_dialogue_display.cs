using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_dialogue_display : MonoBehaviour
{
    public GameObject m_dialogueBox;
    public Text m_nameText;
    public Text m_dialogueText;
    public float m_typeTime = .5f;

    private List<string> m_testText = new List<string>();
    private bool m_isTyping;
    private bool m_writeChar;
    private int m_dialogueIndex;
    private int m_charIndex;
    private string m_textWritten = "";
    private IEnumerator coroutine;



    private void Start()
    {
        /*
        m_testText.Add("It has been 4 hours since I successfully sucked my own penis. Things are different now.");
        m_testText.Add("As soon as mouth-to- penis contact was made I felt a shockwave through my body.");
        StartDialogue(m_testText, "bert");
        */
    }

    void Update()
    {
        // If dialoguebox is active then you can keep progressing through the dialogue
        if (m_dialogueBox.activeInHierarchy)
        {
            if (Input.GetButtonDown("Interact"))
            {
                NextDialogue(m_testText, "bert");
            }
        }
    }


    public void StartDialogue(List<string> textList, string name)
    {
        m_dialogueBox.SetActive(true);
        m_nameText.text = name;
        if (!m_isTyping)
        {
            WriteDialogue(textList[m_dialogueIndex]);
        }

    }

    private void NextDialogue(List<string> textList, string name)
    {
        if (!m_isTyping)
        {
            if ((m_dialogueIndex < textList.Count - 1))
            {
                m_dialogueIndex++;
                m_textWritten = "";

                StartDialogue(textList, name);
            }
            else
            {
                CloseDialogue(); // End of dialogue
            }
        }

        if (m_isTyping)
        {
            m_dialogueText.text = textList[m_dialogueIndex];
            m_textWritten = textList[m_dialogueIndex];
            StopCoroutine(coroutine);
            m_charIndex = 0;
            m_isTyping = false;
            m_writeChar = false;
        }


    }

    public void CloseDialogue()
    {
        m_charIndex = 0;
        m_dialogueText.text = "";
        m_textWritten = "";
        m_charIndex = 0;
        m_dialogueIndex = 0;
        m_writeChar = false;
        m_isTyping = false;
        m_dialogueBox.SetActive(false);
    }

    private void WriteDialogue(string text)
    {

        int textSize = text.Length;

        if (!m_writeChar)
        {
            m_writeChar = true;
            m_charIndex++;
            coroutine = AddNextChar(text, m_typeTime);
            StartCoroutine(coroutine);
        }



    }

    private IEnumerator AddNextChar(string text, float time)
    {
        yield return new WaitForSeconds(time);

        m_writeChar = false;
        m_textWritten += text[m_charIndex - 1];
        m_dialogueText.text = m_textWritten;


        m_isTyping = !m_textWritten.Equals(text);

        if (m_charIndex < text.Length)
        {
            WriteDialogue(text);
        }
        else
        {
            m_charIndex = 0;
        }

    }

}
