using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSounds : MonoBehaviour
{
    public AudioSource hit_sound;

    public void Hit_Sound_Play()
    {
        hit_sound.Play();
    }
}
