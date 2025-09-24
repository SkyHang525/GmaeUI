using UnityEngine;

public class SoundManager
{
    // µ±«∞≤•∑≈µƒ“Ù¿÷√˚
    private string currMusicName = "bgm";

    // “Ù¿÷∫Õ“Ù–ß∂‘œÛ
    private SoundMusic soundMusic;
    private SoundEffects soundEffect;

    // ±≥æ∞“Ù¿÷∫Õ“Ù–ßµƒø™πÿ
    public bool isOpenBGM { get; set; }
    public bool isOpenEffect { get; set; }

    // ‘›Õ£◊¥Ã¨
    private bool soundPause;

    // µ•¿˝ƒ£ Ω
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
    /// ππ‘Ï∫Ø ˝
    /// </summary>
    private SoundManager()
    {
        soundMusic = new SoundMusic("soundMusic");
        soundEffect = new SoundEffects("soundEffect");
    }

    /// <summary>
    /// ≥ı ºªØ
    /// </summary>
    public void Init()
    {
        isOpenBGM = true;
        isOpenEffect = true;
        PlayMusic(currMusicName);
    }

    /// <summary>
    /// ≤•∑≈±≥æ∞“Ù¿÷
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
    /// ‘›Õ£±≥æ∞“Ù¿÷
    /// </summary>
    public void Pause()
    {
        soundPause = true;
        StopMusic();
    }

    /// <summary>
    /// ª÷∏¥±≥æ∞“Ù¿÷
    /// </summary>
    public void Resume()
    {
        soundPause = false;
        soundMusic.Pause();
    }

    /// <summary>
    /// Õ£÷π±≥æ∞“Ù¿÷
    /// </summary>
    public void StopMusic()
    {
        soundMusic.Stop();
    }

    /// <summary>
    /// ≤•∑≈“Ù–ß
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
    /// Õ£÷π“Ù–ß
    /// </summary>
    public void StopEffect()
    {
        soundEffect.Stop();
    }

    /// <summary>
    /// ‘›Õ£“Ù–ß
    /// </summary>
    public void PauseEffect()
    {
        soundEffect.Pause();
    }

    /// <summary>
    /// ª÷∏¥“Ù–ß≤•∑≈
    /// </summary>
    public void ResumeEffect(string effectName)
    {
        PlayEffect(effectName, true);
    }
}
