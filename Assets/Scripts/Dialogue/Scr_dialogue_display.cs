using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Scr_dialogue_display : MonoBehaviour
{
    [Header("DialogueBox")]
    public GameObject m_dialogueBox;
    public Text m_nameText;
    public Text m_dialogueText;
    public float m_typeTime = .5f;

    [Header("Question Box")]
    public EventSystem m_eventSystem;
    public GameObject m_answerBox;
    public Text[] m_AnswerButtonText = new Text[4];

    private List<string> m_dialogue = new List<string>();
    private Scr_dialogue_profile m_currentProfile;
    private bool m_questionOpen;
    private bool m_isTyping;
    private bool m_writeChar;
    private bool m_hasStarted;
    private int m_dialogueIndex;
    private int m_answerIndex;
    private int m_charIndex;
    private int m_questionIndex;
    private string m_name;
    private string m_textWritten = "";
    private IEnumerator coroutine;


    void Update()
    {
        // If dialoguebox is active then you can keep progressing through the dialogue
        if (m_dialogueBox.activeInHierarchy && !m_questionOpen)
        {
            if (Input.GetButtonDown("Interact") && !m_hasStarted )
            {
                NextDialogue(m_currentProfile);
            }
        }
    }

    public void AnswerQuestion(int index)
    {
        m_answerIndex = index;
        KeyValuePair<string,bool> answer = m_currentProfile.GetQuestions()[m_questionIndex].m_Answer[index];
        KeyValuePair<string, bool> newAnswer = new KeyValuePair<string, bool>(answer.Key,true);
        m_currentProfile.GetQuestions()[m_questionIndex].m_Answer[index] = newAnswer;


        m_questionOpen = false;
        m_answerBox.SetActive(false);
        // Disable answerboxes when done
        foreach (var answerBox in m_AnswerButtonText)
        {
            answerBox.transform.parent.gameObject.SetActive(false);
        }
        NextDialogue(m_currentProfile);

    }


    public void StartDialogue(Scr_dialogue_profile profile)
    {     
        if (!m_isTyping && !m_dialogueBox.activeInHierarchy)
        {
            m_currentProfile = profile;
            m_hasStarted = true;
            m_dialogueBox.SetActive(true);
            m_name = profile.Name();
            m_nameText.text = m_name;
            m_dialogueText.font = profile.m_dialogueFont;
            m_nameText.font = profile.m_dialogueFont;
            m_dialogue = profile.Dialogue(); ;
            WriteDialogue(m_dialogue[m_dialogueIndex]);
            Scr_player_controller.FreezePlayer = true;
        }

    }

    private void CheckDialogueCode(List<string> textList)
    {
        string currentLine = textList[m_dialogueIndex];


        if (currentLine.Contains("Scr_"))
            ChangeProfile(textList[m_dialogueIndex]);
        else if (currentLine.Contains("Q:"))
            StartQuestion(m_currentProfile);
        else
            WriteDialogue(textList[m_dialogueIndex]);
    }

    private void ChangeProfile(string nextProfile)
    {
        int index = System.Convert.ToInt32(nextProfile.Remove(1));
        nextProfile = nextProfile.Remove(0, 1);
        Scr_dialogue_profile newProfile = GetComponent(nextProfile) as Scr_dialogue_profile;
        m_currentProfile = newProfile;
        m_dialogueIndex = index;
        m_dialogueText.font = newProfile.m_dialogueFont;
        m_nameText.font = newProfile.m_dialogueFont;
        m_dialogueText.text = "";
        m_dialogue = newProfile.Dialogue();
        m_dialogue.TrimExcess();
        m_name = newProfile.Name();
        m_nameText.text = m_name;

        WriteDialogue(m_dialogue[m_dialogueIndex]);
    }

    private void StartQuestion(Scr_dialogue_profile profile)
    {
        m_answerBox.SetActive(true);
        m_questionOpen = true;
        m_questionIndex = System.Convert.ToInt32(m_dialogue[m_dialogueIndex].Remove(0, 2));
        Question question = profile.GetQuestions()[m_questionIndex];
        m_dialogueText.text = question.m_text;
      
        // Write answers on the buttons
        for (int i = 0; i < question.m_answerAmount; i++)
        {
            m_AnswerButtonText[i].transform.parent.gameObject.SetActive(true);
            m_AnswerButtonText[i].text = question.m_Answer[i].Key; // Set the text on the button
        }
        Button firstButton = m_AnswerButtonText[0].transform.parent.GetComponent<Button>();
        // Make the first answer button selected and highlighted at start
        firstButton.Select();
        firstButton.OnSelect(null);
        
    }



    private void NextDialogue(Scr_dialogue_profile profile)
    {
        m_dialogue = profile.Dialogue();
        if (!m_isTyping)
        {
            if ((m_dialogueIndex < m_dialogue.Count - 1))
            {
                
                m_dialogueIndex++;
                m_textWritten = "";
                CheckDialogueCode(m_dialogue);
                      
            }
            else
            {
                CloseDialogue(); // End of dialogue
            }
        }

        if (m_isTyping) // Speed up dialogue
        {
            m_dialogueText.text = m_dialogue[m_dialogueIndex];
            m_textWritten = m_dialogue[m_dialogueIndex];
            StopCoroutine(coroutine);
            m_charIndex = 0;
            m_isTyping = false;
            m_writeChar = false;
        }


    }

    public void CloseDialogue()
    {
        m_hasStarted = false;
        m_charIndex = 0;
        m_dialogueText.text = "";
        m_textWritten = "";
        m_charIndex = 0;
        m_dialogueIndex = 0;
        m_writeChar = false;
        m_isTyping = false;
        m_dialogueBox.SetActive(false);
        Scr_player_controller.FreezePlayer = false;
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
        m_hasStarted = false;
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
