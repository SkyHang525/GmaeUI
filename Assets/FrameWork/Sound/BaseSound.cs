using UnityEngine;

public class BaseSound
{
    protected float volume = 1f;
    protected AudioSource audioSource;

    public BaseSound(string nodeName)
    {
        GameObject audioMgr = new GameObject(nodeName);

        try
        {
            // ���ڵ���ӵ�����
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in BaseSound constructor: " + e.ToString());
        }

        // ���ڵ���Ϊ��פ�ڵ㣬ȷ���ڳ����л�ʱ��������
        Object.DontDestroyOnLoad(audioMgr);

        // ��� AudioSource ��������ڲ�����Ƶ
        this.audioSource = audioMgr.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = false;
    }

    /// <summary>
    /// ��ȡ��Ч��Դ
    /// </summary>
    /// <param name="key">��Ч��Դ��</param>
    public AudioClip GetSound(string key)
    {
        AudioClip sound = ResMgr.Instance.GetAsset<AudioClip>(key);

        if (sound == null)
        {
            OnResourceLoadComplete(key);
        }

        return sound;
    }

    /// <summary>
    /// ��Դ�������ʱ�Ļص�
    /// </summary>
    private async void OnResourceLoadComplete(string key)
    {
        try
        {
            AudioClip clip = await ResMgr.Instance.AwaitGetAsset<AudioClip>(key);
            LoadedPlay(key);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load sound: " + e.Message);
        }
    }

    /// <summary>
    /// ��Դ������ɺ���в���
    /// </summary>
    public void LoadedPlay(string key)
    {
        Play(key);
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    public virtual void Play(string soundName, bool loop = false)
    {
        AudioClip sound = GetSound(soundName);

        if (sound != null)
        {
            PlaySound(sound, loop);
        }
    }

    /// <summary>
    /// ֹͣ��Ч����
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// ��ͣ��Ч����
    /// </summary>
    public void Pause()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// ������Ч�����������д�˷���
    /// </summary>
    protected virtual void PlaySound(AudioClip sound, bool loop = false)
    {
        audioSource.clip = sound;
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void SetVolume(float volume)
    {
        this.volume = volume;
        audioSource.volume = volume;
    }
}
