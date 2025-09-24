using UnityEngine;

public class SoundMusic : BaseSound
{
    public string currName;

    public SoundMusic(string nodeName) : base(nodeName)
    {
    }

    /// <summary>
    /// ����ָ������
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

        Stop();  // ֹͣ��ǰ���ŵ�����

        AudioClip sound = GetSound(soundName);
        if (sound != null)
        {
            currName = soundName;
            PlaySound(sound);  // ����������
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    protected override void PlaySound(AudioClip sound, bool loop = false)
    {
        if (audioSource == null || sound == null)
        {
            return;
        }

        audioSource.clip = sound;
        audioSource.loop = true;  // ����ѭ������
        audioSource.Play();  // ��������
    }

    /// <summary>
    /// ��������
    /// </summary>
    public new void SetVolume(float volume)
    {
        base.SetVolume(volume);  // ���ø���ķ�����������
        audioSource.volume = volume;
    }
}
