using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scr_dialogue_profile : MonoBehaviour
{
    /// <summary>
    /// Dialogue Instructions:
    /// Q:number / writing this on a line will start the question on that number, example (Q:1)  
    /// numberScr_profile_name / this will change the dialogue to another profile on the line from number, example (2Scr_profile_bert)
    /// 
    /// </summary>
    public List<string> m_dialogue = new List<string>();
    public List<Question> m_questions = new List<Question>();

    private void Awake()
    {
        m_dialogue = Dialogue();
        m_questions = SetQuestions();
    }

    public List<Question> GetQuestions()
    {
        return m_questions;
    }
    public abstract string Name();
    public abstract List<Question> SetQuestions();
    public abstract List<string> Dialogue();


}



public class Question
{
    public string m_text;
    public int m_answerAmount;

    public KeyValuePair<string,bool>[] m_Answer = new KeyValuePair<string, bool>[4];
}
