using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class CinemaController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;

    public void Play()
    {
        foreach(PlayableDirector playableDirector in playableDirectors)
            playableDirector.Play();
    }

    public void PlayFromTimelins(int index)
    {
        TimelineAsset selectedAsset;

        if (timelines.Count <= index) {
            selectedAsset = timelines[timelines.Count - 1];
        }
        selectedAsset = timelines[index];
    }
}
