using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_dialogue : MonoBehaviour, IInteract
{
    public static bool CanStartDialogue;

    public string m_profileName;

    private Scr_dialogue_display m_dialogueDisplay;
    private Scr_dialogue_profile m_profile;
    

    // Start is called before the first frame update
    void Start()
    {
        m_dialogueDisplay = FindObjectOfType<Scr_dialogue_display>();
        m_profile = m_dialogueDisplay.GetComponent(m_profileName) as Scr_dialogue_profile;
    }

    public void Interact()
    {
        if (CanStartDialogue)
            m_dialogueDisplay.StartDialogue(m_profile);
    }
}
