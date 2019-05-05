using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scr_dialogue_profile : MonoBehaviour
{
    public string m_name;
    public List<string> m_dialogue = new List<string>();
    public List<Question> m_questions = new List<Question>();
    public int m_dialogueIndex = 0;


    public abstract List<Question> AddQuestions();
    public abstract List<string> AddDialogue();


}



public class Question
{
    public string m_text;
    public int m_answerAmount;

    public KeyValuePair<string,bool> m_Answer1,m_Answer2,m_Answer3,m_Answer4;
}
