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
    //TODO: you will need more data than this, like clips to play and a way to know which clip is playing
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


    //TODO; prep the boom box
}

public void PlayClip()
{
        //Play a current song in the array
        audioSource.clip = audios[song];
        audioSource.Play();
}

private void Update()
{
        //If the boombox is playing and interacted with, the next audioclip attached is played
        if (!audioSource.isPlaying && isUsed)
        {
            song++;
            
            //If the last audioclip is played, then the first clip is played again
            if (song > audios.Length - 1)
            {
                song = 0;
            }

            PlayClip();
        }
    //TODO; this is where you might want to setup and ensure the desire clip is playing on the source
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

    //TODO; this where we need to go to next track and start and stop playing
    }
}
