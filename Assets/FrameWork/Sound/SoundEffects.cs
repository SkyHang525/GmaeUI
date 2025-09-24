using UnityEngine;

public class SoundEffects : BaseSound
{
    public SoundEffects(string nodeName) : base(nodeName)
    {
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="sound">��Ч</param>
    /// <param name="loop">�Ƿ�ѭ������</param>
    protected override void PlaySound(AudioClip sound, bool loop = false)
    {
        if (audioSource == null || sound == null)
        {
            return;
        }

        if (loop)
        {
            audioSource.clip = sound;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(sound, volume);
        }
    }
}
