using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource audio_Button;
    [SerializeField]
    private AudioSource audio_Feedback;
    [SerializeField]
    private AudioSource audio_background;

    [SerializeField]
    private List<AudioClip> ButtonClicksSounds = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> FeedbackSounds = new List<AudioClip>();

    [SerializeField]
    private AudioClip backgroundMusic;


    private void Start()
    {
        GetButtonSounds();
        GetFeedbackSounds();
    }

    #region Load Sounds from Resources
    private void GetButtonSounds()
    {
        UnityEngine.Object[] audioClips = Resources.LoadAll("Audio/ButtonClicks", typeof(AudioClip));
        foreach (UnityEngine.Object audioclip in audioClips)
        {
            ButtonClicksSounds.Add((AudioClip)audioclip);
        }
    }
    private void GetFeedbackSounds()
    {
        UnityEngine.Object[] audioClips = Resources.LoadAll("Audio/FeedbackSound", typeof(AudioClip));
        foreach (UnityEngine.Object audioclip in audioClips)
        {
            FeedbackSounds.Add((AudioClip)audioclip);
        }
    }

    #endregion

    public void PlayButtonSound()
    {
        audio_Button.Stop();
        audio_Button.clip = ButtonClicksSounds[Random.Range(0, ButtonClicksSounds.Count)];
        audio_Button.Play();
    }

    public void PlayFeedbackSound(bool Feedback) // 1 = positive, 0 = negative
    {
        audio_Feedback.Stop();

        if (Feedback)
            audio_Feedback.clip = FeedbackSounds[1];
        else
            audio_Feedback.clip = FeedbackSounds[0];

        audio_Feedback.Play();
    }

    public void Playbackground()
    {
        audio_background.Play();
    }
}
