using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Song", menuName = "ScriptableObjects/SongData", order = 2)]
public class SongData : ScriptableObject
{
    public string SONG_NAME;
    public TextAsset OSU_FILE;
    public AudioClip AUDIO_TRACK;

    public int BEAT_MULTIPLIER;
}
