using UnityEngine;

public class SoundMusic : BaseSound
{
    public string currName;

    public SoundMusic(string nodeName) : base(nodeName)
    {
    }

    /// <summary>
    /// 播放指定音乐
    /// </summary>
    public override void Play(string soundName, bool loop = false)
    {
        if (audioSource == null)
        {
            return;
        }

        if (currName == soundName && audioSource.isPlaying)
        {
            return;
        }

        Stop();  // 停止当前播放的音乐

        AudioClip sound = GetSound(soundName);
        if (sound != null)
        {
            currName = soundName;
            PlaySound(sound);  // 播放新音乐
        }
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    protected override void PlaySound(AudioClip sound, bool loop = false)
    {
        if (audioSource == null || sound == null)
        {
            return;
        }

        audioSource.clip = sound;
        audioSource.loop = true;  // 设置循环播放
        audioSource.Play();  // 播放音乐
    }

    /// <summary>
    /// 设置音量
    /// </summary>
    public new void SetVolume(float volume)
    {
        base.SetVolume(volume);  // 调用父类的方法设置音量
        audioSource.volume = volume;
    }
}
