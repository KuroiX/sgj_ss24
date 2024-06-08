using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [SerializeField] private TextBox textBox;
        [SerializeField] private float timeBeforePlayingVoiceClip = 1;
        [SerializeField] private float timeAfterPlayingVoiceClip = 1;

        private float _clipDelay;
        private float _resetDelay;
        private AudioSource _audioSource;
        

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;

            _audioSource = GetComponent<AudioSource>();
            _clipDelay = textBox.GetDuration() + timeBeforePlayingVoiceClip;
            _resetDelay = _clipDelay + timeAfterPlayingVoiceClip;
        }

        [ContextMenu("show voice line")]
        public void ShowVoiceLine(VoiceLine voiceLine)
        {
           
            textBox.DisplayTextBox(voiceLine.textLine, voiceLine.icon);
            AudioClip currentClip = voiceLine.audioClip;
            _audioSource.clip = currentClip;

            var length = currentClip
                ? currentClip.length
                : 0;
            
            Invoke(nameof(PlayClip), _clipDelay);
            Invoke(nameof(ResetAll), _resetDelay + length);
        }

        private void PlayClip()
        {
            Debug.Log("playing audio clip...");
            // _audioSource.Play();
        }

        private void ResetAll()
        {
            Debug.Log("resetting box and clip");
            textBox.HideTextBox();
            _audioSource.clip = null;
        }
    }
}