using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song
{
    public struct TimingPoint{
    // time (Integer): Start time of the timing section, in milliseconds from the beginning of the beatmap's audio. The end of the timing section is the next timing point's time (or never, if this is the last timing point).
    // beatLength (Decimal): This property has two meanings:
    //     For uninherited timing points, the duration of a beat, in milliseconds.
    //     For inherited timing points, a negative inverse slider velocity multiplier, as a percentage. For example, -50 would make all sliders in this timing section twice as fast as SliderMultiplier.
    // meter (Integer): Amount of beats in a measure. Inherited timing points ignore this property.
    // sampleSet (Integer): Default sample set for hit objects (0 = beatmap default, 1 = normal, 2 = soft, 3 = drum).
    // sampleIndex (Integer): Custom sample index for hit objects. 0 indicates osu!'s default hitsounds.
    // volume (Integer): Volume percentage for hit objects.
    // uninherited (0 or 1): Whether or not the timing point is uninherited.
    // effects (Integer): Bit flags that give the timing point extra effects. See the effects section.
        public int time;
        public decimal beatLength;
        public int meter;
        public int sampleSet;
        public int sampleIndex;
        public int volume;
        public bool uninherited;
        public int effects;
    }
    public List<TimingPoint> timingPoints = new();

    public struct HitObject{
    // x (Integer) and y (Integer): Position in osu! pixels of the object.
    // time (Integer): Time when the object is to be hit, in milliseconds from the beginning of the beatmap's audio.
    // type (Integer): Bit flags indicating the type of the object. See the type section.
    // hitSound (Integer): Bit flags indicating the hitsound applied to the object. See the hitsound section.
    // objectParams (Comma-separated list): Extra parameters specific to the object's type.
    // hitSample (Colon-separated list): Information about which samples are played when the object is hit. It is closely related to hitSound; see the hitsounds section. If it is not written, it defaults to 0:0:0:0:.
        public int x;
        public int y;
        public int time;
        public int type;
        public int hitSound;
        public string objectParams;
        public string hitSample;
    }
    public List<HitObject> hitObjects = new();
}
