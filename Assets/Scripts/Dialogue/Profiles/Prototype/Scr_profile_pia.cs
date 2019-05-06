using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_profile_pia : Scr_dialogue_profile
{
    public override List<string> Dialogue()
    {
        List<string> dialogue = new List<string>();
        dialogue.Add("Hej jag heter pia, fan va fullt namn du har."); // 0
        dialogue.Add("2Scr_profile_bert"); // 1
        dialogue.Add("Jag gillar bira."); // 2
        dialogue.Add("bira är bra."); // 3
        dialogue.Add("4Scr_profile_bert"); // 4
        return dialogue;
    }

    public override string Name()
    {
        string name = "Pia";

        return name;
    }

    public override List<Question> SetQuestions()
    {
        List<Question> questions = new List<Question>();

        return questions;
    }

}
