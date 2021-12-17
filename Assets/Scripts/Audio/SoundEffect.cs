using UnityEngine;

namespace Overgrown.Audio
{
    [CreateAssetMenu(fileName = "Sound Effect", menuName = "Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        public SoundType type;
        public AudioClip[] sounds;

        public AudioClip GetRandomClip()
        {
            var length = sounds.Length;
            var randomIndex = UnityEngine.Random.Range(0, length);
            return sounds[randomIndex];
        }
    }
}
