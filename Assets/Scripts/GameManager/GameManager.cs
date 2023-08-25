using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Net.WebSockets;
using System;

public class GameManager : MonoBehaviour
{
    public SongData currentSong;
    public string OsuRaw;
    // Start is called before the first frame update
    void Start()
    {
        OsuRaw = currentSong.OSU_FILE.ToString();
        Debug.Log(OsuRaw);
        OsuSongParse(OsuRaw);
    }
    Song OsuSongParse(string rawOsuString){
        Song song = new Song();
        int startScanPlace = rawOsuString.IndexOf("[TimingPoints]");
        int endScanPlace = Regex.Match(rawOsuString.Substring(startScanPlace), @"(?<=\r?\n)[ \t]*(\r?\n|$)").Index - 1;
        //Debug.Log(startScanPlace + "," + endScanPlace);
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
        //Debug.Log(startScanPlace + "," + endScanPlace);
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

        return new Song();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
