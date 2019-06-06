using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scr_ui_dialoguebox_init : MonoBehaviour
{

    public TMPro.TextMeshProUGUI m_nameText, m_dialogueText;
    public GameObject m_answerBox;
    public TMPro.TextMeshProUGUI[] m_AnswerButtonText = new TMPro.TextMeshProUGUI[4];

    private Scr_dialogue_display m_dialougeManager;
    private GameObject m_dialogueBox;

   
    // Start is called before the first frame update
    void OnEnable()
    {
        m_dialougeManager = FindObjectOfType<Scr_dialogue_display>();
        InsertIntoManager();

    }

    void InsertIntoManager()
    {
        m_dialougeManager.m_nameText = m_nameText;
        m_dialougeManager.m_dialogueText = m_dialogueText;
        m_dialougeManager.m_answerBox = m_answerBox;
        m_dialougeManager.m_AnswerButtonText = m_AnswerButtonText;

    }
}
