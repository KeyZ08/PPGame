using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource hit_sound;
    public AudioSource step_sound;
    public AudioSource damage_sound;
    public AudioSource death_sound;

    public void Hit_Sound_Play()
    {
        hit_sound.Play();
    }

    public void Damage_Sound_Play()
    {
        damage_sound.Play();
    }

    public void Step_Sound_Play()
    {
        step_sound.pitch = Random.Range(0.9f, 1.1f);
        step_sound.Play();
    }

    public void Death_Sound_Play()
    {
        death_sound.Play();
    }
}
