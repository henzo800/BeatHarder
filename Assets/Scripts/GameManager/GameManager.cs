using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SongData currentSongData;
    public BoxCollider spawnArea;
    public GameObject attackPrefab;
    public float timeSinceStart;
    public Song currentSong;
    AudioSource audioSource;
    
    public float getAudioSource() {
        return this.audioSource.time;
    }
    void Awake() {
        if(instance == null){
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        string OsuRaw = currentSongData.OSU_FILE.ToString();

        currentSong = OsuSongParse(OsuRaw);
        audioSource.clip = currentSongData.AUDIO_TRACK;
        audioSource.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Enemy Attack Pattern based on the osu mapping timing
        timeSinceStart = audioSource.time;
        foreach(Song.HitObject hitObject in currentSong.hitObjects.ToArray()){
            if(hitObject.time/1000 <= timeSinceStart){
                Instantiate(attackPrefab, new Vector3((((float)hitObject.x-256)/512) * 10, 0, (((float)hitObject.y-192)/384) * 10), Quaternion.identity);
                currentSong.hitObjects.Remove(hitObject);
            }
        }
    }

    Song OsuSongParse(string rawOsuString){
        Debug.Log("(-) Parsing OSU File");
        Song song = new();
        int startScanPlace = rawOsuString.IndexOf("[TimingPoints]");
        int endScanPlace = Regex.Match(rawOsuString.Substring(startScanPlace), @"(?<=\r?\n)[ \t]*(\r?\n|$)").Index - 1;
        Debug.Log(rawOsuString.Substring(startScanPlace, endScanPlace));
        string[] TimingPointsRaw = rawOsuString.Substring(startScanPlace, endScanPlace).Split("\n");
        for(int i = 1; i < TimingPointsRaw.Length - 1; i++){
            string[] values = TimingPointsRaw[i].Split(",");
            Song.TimingPoint timingPoint = new()
            {
                time = int.Parse(values[0]),
                beatLength = decimal.Parse(values[1]),
                meter = int.Parse(values[2]),
                sampleSet = int.Parse(values[3]),
                sampleIndex = int.Parse(values[4]),
                volume = int.Parse(values[5]),
                uninherited = Convert.ToBoolean(int.Parse(values[6])),
                effects = int.Parse(values[7])
            };
            song.timingPoints.Add(timingPoint); 
        }

        startScanPlace = rawOsuString.IndexOf("[HitObjects]");
        endScanPlace = Regex.Match(rawOsuString.Substring(startScanPlace), @"(?<=\r?\n)[ \t]*(\r?\n|$)").Index;
        Debug.Log(rawOsuString.Substring(startScanPlace, endScanPlace));
        string[] HitObjectsRaw = rawOsuString.Substring(startScanPlace, endScanPlace).Split("\n");
        for(int i = 1; i < HitObjectsRaw.Length - 1; i++){
            string[] values = HitObjectsRaw[i].Split(",");
            Song.HitObject hitObject = new()
            {
                x = int.Parse(values[0]),
                y = int.Parse(values[1]),
                time = int.Parse(values[2]),
                hitSound = int.Parse(values[3]),
                objectParams = values[4],
                hitSample = values[5]
            };
            song.hitObjects.Add(hitObject); 
        }
        Debug.Log("(-) Done Processing Osu File");
        return song;
    }
}
