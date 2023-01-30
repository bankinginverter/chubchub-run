using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Unity Declarations

        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [SerializeField] private AudioClip[] audioClipsInput_BackgroundMusic;

        [SerializeField] private AudioClip[] audioClipsInput_FX;

        [SerializeField] private AudioSource audio_BackgroundMusic;

        [SerializeField] private AudioSource audio_FX;
 
    #endregion

    #region Private Methods

        public void SetBackgroundAudio(int index)
        {
            audio_BackgroundMusic.clip = audioClipsInput_BackgroundMusic[index];

            audio_BackgroundMusic.Play();
        }

        public void SetFXAudio(int index)
        {
            audio_FX.PlayOneShot(audioClipsInput_FX[index]);
        }

    #endregion
}
