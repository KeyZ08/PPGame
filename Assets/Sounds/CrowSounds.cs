using UnityEngine;

public class CrowSounds : MonoBehaviour
{
    public AudioSource cast_sound;
    public AudioSource fly_sound;
    public AudioSource rise_sound;

    public void Cast_Sound_Play()
    {
        cast_sound.Play();
    }

    public void Rise_Sound_Play()
    {
        rise_sound.Play();
    }

    public void Fly_Sound_Play()
    {
        fly_sound.Play();
    }
}
