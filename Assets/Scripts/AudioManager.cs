using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sources Settings")]
    [Tooltip("Main music audio source")]
    [SerializeField]
    private AudioSource music;
    [Tooltip("Main effect audio source")]
    [SerializeField]
    private List<AudioSource> effects;

    void Start()
    {
        MakeSingleton();
    }

    // Convierte el objeto en singleton
    private void MakeSingleton()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //Reproduce un clip de musica
    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    //Reproduce un clip efecto de sonido
    public void PlaySound(AudioClip clip)
    {
        AudioSource mSource = SoundCheck();
        if (mSource != null)
        {
            mSource.clip = clip;
            mSource.Play();
        }

    }

    //Chequea si se reproduce un sonido
    private AudioSource SoundCheck()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (!effects[i].isPlaying)
                return effects[i];
        }
        return null;
    }
}
