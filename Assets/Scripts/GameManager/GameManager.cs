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
    public List<GameObject> Minions;
    // public List<GameObject> BossStrike;

    public float timeSinceStart;
    public Song currentSong;
    public AudioSource audioSource;
    public float beatLength;
    public float arenaSize;

    // attack objects
    public GameObject pinIndicator; // animation for pin
    public GameObject pinDamage; // damage object
    public GameObject sweepParticle; // particle for sweep
    public int numParticles = 24;

    // sound effects
    public AudioClip sweepAttackSound;

    public float getAudioSource() {
        return this.audioSource.time;
    }
    void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        string OsuRaw = currentSongData.OSU_FILE.ToString();

        currentSong = OsuSongParse(OsuRaw);
        audioSource.clip = currentSongData.AUDIO_TRACK;
        audioSource.Play();

        beatLength = (float)currentSong.timingPoints[0].beatLength;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Enemy Attack Pattern based on the osu mapping timing
        timeSinceStart = audioSource.time;
        foreach(Song.HitObject hitObject in currentSong.hitObjects.ToArray()){
            if(hitObject.time/1000 <= timeSinceStart){
                switch(hitObject.type){
                    case 0:
                    // Instantiate(BossStrike[0], new Vector3((((float)hitObject.x-256)/512) * arenaSize, 0, (((float)hitObject.y-192)/384) * arenaSize), Quaternion.identity);
                        PinAttack(new Vector3((((float)hitObject.x-256)/512) * arenaSize, 0, (((float)hitObject.y-192)/384) * arenaSize), beatLength * 4f * 0.001f);
                        break;
                    case 1:
                        Instantiate(Minions[UnityEngine.Random.Range(0,1)], new Vector3((((float)hitObject.x-256)/512) * arenaSize, 0, (((float)hitObject.y-192)/384) * arenaSize), Quaternion.identity);
                        break;
                    case 3:
                        // Instantiate(BossSpecial, new Vector3((((float)hitObject.x-256)/512) * arenaSize, 0, (((float)hitObject.y-192)/384) * arenaSize), Quaternion.identity);
                        SweepAttack();
                        break;
                }
                currentSong.hitObjects.Remove(hitObject);
            }
        }
    }

    void PinAttack(Vector3 position, float indicatorLength) {
        // start indicator animation
        Instantiate(pinIndicator, position + new Vector3(0f, 0.01f, 0f), Quaternion.Euler(0f, 0f, 0f));
        //Debug.Log("Pin spawned at " + position.x + ", " + position.z);

        // do damage
        StartCoroutine(PinDamage(position, indicatorLength));
    }

    IEnumerator PinDamage(Vector3 position, float indicatorLength) {
        yield return new WaitForSeconds(indicatorLength);
        //Debug.Log("Spawned pin damage");
        Instantiate(pinDamage, position, Quaternion.Euler(0f, 0f, 0f));
    }

    // Sweep
    void SweepAttack() {
        audioSource.PlayOneShot(sweepAttackSound, 1f);

        Transform BossTransform = BossController.instance.transform;
        Vector3 position = BossTransform.position + new Vector3(0f, 0.5f, 0f);
        BossController.instance.transform.rotation = Quaternion.Euler(0,90f,0f);
        for (int i = 0; i < numParticles; i++) {
            BossController.instance.transform.rotation = Quaternion.Euler(0,90+i*180/numParticles,0);
            GameObject particle = Instantiate(sweepParticle, position, BossController.instance.transform.rotation);
        }
        BossController.instance.transform.rotation = Quaternion.Euler(0f,0f,0f);
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
