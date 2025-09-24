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
            // 将节点添加到场景
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in BaseSound constructor: " + e.ToString());
        }

        // 将节点标记为常驻节点，确保在场景切换时不会销毁
        Object.DontDestroyOnLoad(audioMgr);

        // 添加 AudioSource 组件，用于播放音频
        this.audioSource = audioMgr.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = false;
    }

    /// <summary>
    /// 获取音效资源
    /// </summary>
    /// <param name="key">音效资源名</param>
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
    /// 资源加载完成时的回调
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
    /// 资源加载完成后进行播放
    /// </summary>
    public void LoadedPlay(string key)
    {
        Play(key);
    }

    /// <summary>
    /// 播放音效
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
    /// 停止音效播放
    /// </summary>
    public void Stop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// 暂停音效播放
    /// </summary>
    public void Pause()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// 播放音效，子类可以重写此方法
    /// </summary>
    protected virtual void PlaySound(AudioClip sound, bool loop = false)
    {
        audioSource.clip = sound;
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// 设置音量
    /// </summary>
    public void SetVolume(float volume)
    {
        this.volume = volume;
        audioSource.volume = volume;
    }
}
