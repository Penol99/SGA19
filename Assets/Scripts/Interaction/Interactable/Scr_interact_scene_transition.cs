using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scr_interact_scene_transition : MonoBehaviour, IInteract
{
    public Vector3 m_spawnPosition;
    public Object m_newScene;
    public bool m_transitionOnCollide;
    public LayerMask m_playerLayer;


    private Scr_scene_manager m_sceneManager;



    // Start is called before the first frame update
    void Start()
    {
        m_sceneManager = FindObjectOfType<Scr_scene_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_transitionOnCollide)
        {
            bool playerCollide = Physics.CheckBox(transform.position, transform.localScale / 2,transform.rotation,m_playerLayer);
            if (playerCollide)
            {
                m_sceneManager.LoadScene(m_newScene.name,m_spawnPosition);
            }
        }
    }

    public void Interact()
    {
        if(!m_transitionOnCollide)
            m_sceneManager.LoadScene(m_newScene.name,m_spawnPosition);
    }
}
