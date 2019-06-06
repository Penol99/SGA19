using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Scr_cutscene_manager : MonoBehaviour
{
    public static bool CutscenePlaying;
    public PlayableDirector[] m_timelines;

    private int m_timelineIndex;

    public void PlayTimeline(int index)
    {
        
        m_timelineIndex = index;
        m_timelines[m_timelineIndex].played += TimelinePlayed;
        m_timelines[m_timelineIndex].stopped += TimelineEnded;
        m_timelines[index].Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PlayTimeline(0);
    }

    private void TimelinePlayed(PlayableDirector director)
    {
        if (m_timelines[m_timelineIndex] == director)
        {
            Scr_player_controller.FreezePlayer = true;           

            CutscenePlaying = true;
            m_timelines[m_timelineIndex].played -= TimelinePlayed;
        }
    }

    private void TimelineEnded(PlayableDirector director)
    {
        if (m_timelines[m_timelineIndex] == director)
        {
            Scr_player_controller.FreezePlayer = false;

            CutscenePlaying = false;
            m_timelines[m_timelineIndex].stopped -= TimelineEnded;
        }
    }

}
