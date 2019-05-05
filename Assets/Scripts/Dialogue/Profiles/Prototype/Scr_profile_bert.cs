using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_profile_bert : Scr_dialogue_profile
{

    private void Start()
    {

    }


    public override List<string> Dialogue()
    {
        List<string> dialogue = new List<string>();
        dialogue.Add("Hej jag heter bert, vad heter du?"); // 0
        dialogue.Add("0Scr_profile_pia"); // 1
        dialogue.Add("Du luktar bira, pia."); // 2
        dialogue.Add("2Scr_profile_pia"); // 3
        dialogue.Add("Det är det verkligen inte."); // 4
        dialogue.Add("Du är en osund jävel."); // 5
        dialogue.Add("Q:0"); // 6


        return dialogue;
    }

    public override List<Question> SetQuestions()
    {
        List<Question> questions = new List<Question>();
        Question q0 = new Question();
        q0.m_answerAmount = 2;
        q0.m_text = "Tycker du pia luktar bira?";
        q0.m_Answer[0] = new KeyValuePair<string, bool>("Yes",false);
        q0.m_Answer[1] = new KeyValuePair<string, bool>("No",false);
        questions.Add(q0);


        return questions;
    }

    public override string Name()
    {
        string name = "Bert";

        return name;
    }
}
