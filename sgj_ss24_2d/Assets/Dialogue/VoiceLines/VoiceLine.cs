using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu]
    public class VoiceLine : ScriptableObject
    {
        public AudioClip audioClip;
        public string shit;
        public Sprite icon;
        public string textLine;
    }
}

