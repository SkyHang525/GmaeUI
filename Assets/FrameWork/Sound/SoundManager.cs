using UnityEngine;

public class SoundManager
{
    // ��ǰ���ŵ�������
    private string currMusicName = "bgm";

    // ���ֺ���Ч����
    private SoundMusic soundMusic;
    private SoundEffects soundEffect;

    // �������ֺ���Ч�Ŀ���
    public bool isOpenBGM { get; set; }
    public bool isOpenEffect { get; set; }

    // ��ͣ״̬
    private bool soundPause;

    // ����ģʽ
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SoundManager();
            return _instance;
        }
    }

    /// <summary>
    /// ���캯��
    /// </summary>
    private SoundManager()
    {
        soundMusic = new SoundMusic("soundMusic");
        soundEffect = new SoundEffects("soundEffect");
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Init()
    {
        isOpenBGM = true;
        isOpenEffect = true;
        PlayMusic(currMusicName);
    }

    /// <summary>
    /// ���ű�������
    /// </summary>
    public void PlayMusic(string bgName)
    {
        if (!isOpenBGM || soundPause)
        {
            return;
        }
        currMusicName = bgName;
        soundMusic.Play(bgName);
    }

    /// <summary>
    /// ��ͣ��������
    /// </summary>
    public void Pause()
    {
        soundPause = true;
        StopMusic();
    }

    /// <summary>
    /// �ָ���������
    /// </summary>
    public void Resume()
    {
        soundPause = false;
        soundMusic.Pause();
    }

    /// <summary>
    /// ֹͣ��������
    /// </summary>
    public void StopMusic()
    {
        soundMusic.Stop();
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    public void PlayEffect(string effectName, bool loop = false)
    {
        if (!isOpenEffect || soundPause)
        {
            return;
        }
        soundEffect.Play(effectName, loop);
    }

    /// <summary>
    /// ֹͣ��Ч
    /// </summary>
    public void StopEffect()
    {
        soundEffect.Stop();
    }

    /// <summary>
    /// ��ͣ��Ч
    /// </summary>
    public void PauseEffect()
    {
        soundEffect.Pause();
    }

    /// <summary>
    /// �ָ���Ч����
    /// </summary>
    public void ResumeEffect(string effectName)
    {
        PlayEffect(effectName, true);
    }
}
