using UnityEngine;

public class SoundEffects : BaseSound
{
    public SoundEffects(string nodeName) : base(nodeName)
    {
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="sound">音效</param>
    /// <param name="loop">是否循环播放</param>
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
