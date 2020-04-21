using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that when used changes to the next song, when out of songs turns off, when used while off, plays first song.
/// 
/// TODO; It should auto play, randomise order potentially and go to next track when used.
///     In other words, act kind of like the radio in a GTA style game.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BoomBoxItem : InteractiveItem
{
    protected AudioSource audioSource;
    [SerializeField] AudioClip[] audios;
    int song = -1;
    bool isUsed = false;

protected override void Start()
    {
       base.Start();
        
        //Retrieve the data from the AudioSource component and create an array for the attached clips
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audios[0];
    }

public void PlayClip()
    {
        //Play a current song in the array
        audioSource.clip = audios[song];
        audioSource.Play();
    }

private void Update()
    {
        //If the boombox is not playing and interacted with, the next audioclip attached is played
        if (!audioSource.isPlaying && isUsed)
        {
            song++;

            //If the last audioclip is played, then the current clip is reset to the first
            if (song > audios.Length - 1)
            {
                song = 0;
            }

            PlayClip();
        }
    }

public override void OnUse()
    {
        base.OnUse();
        //Return true if boombox object is used
        isUsed = true;

        //If the current audioclip is not the last audioclip, then play the next audioclip
        if (song < audios.Length - 1)
        {
            song++;
            PlayClip();
        }
        //If not then stop playing, and reset the song variable to the first audioclip and the isUsed variable as false
        else
        {
            audioSource.Stop();
            song = 0;
            isUsed = false;
        }
     }
}
