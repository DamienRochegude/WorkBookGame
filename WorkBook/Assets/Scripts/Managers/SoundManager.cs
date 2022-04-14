using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : GenericSingletonClass<SoundManager>
{
    private const string musicpath = "Musics/";

    enum Sound : byte
    {
        damage = 0,
        death_player = 1,
        eraser = 2,
        dash = 3,
        page_flip = 4,
        push_button = 5,
        write = 6,
        jump = 7
    }

    static private AudioSource audioSourceSound;
    static private AudioSource audioSourceMusic;
    static private Music music;
    static private byte currentIndex = 5;
    static private bool isPaused = false;
    
    static private AudioClip[] lstUISounds;
    static public AudioClip[] LstUISounds
    {
        set { lstUISounds = value; }
    }

    static private AudioMixerGroup audioMixer;
    static public AudioMixerGroup AudioMixer
    {
        set { audioMixer = value; }
    }

    static private string[] lstFilesName;
    static public string[] LstFilesName
    {
        set { lstFilesName = value; }
    }

    private void Start()
    {
        GameObject audioSourceMusicGO = new GameObject();
        audioSourceMusicGO.name = "Audio Source Music";
        audioSourceMusic = audioSourceMusicGO.AddComponent<AudioSource>();
        audioSourceMusic.outputAudioMixerGroup = audioMixer;

        GameObject audioSourceSoundGO = new GameObject();
        audioSourceSoundGO.name = "Audio Source Sound";
        audioSourceSound = audioSourceSoundGO.AddComponent<AudioSource>();
        audioSourceSound.outputAudioMixerGroup = audioMixer;

        DontDestroyOnLoad(audioSourceMusicGO);
        DontDestroyOnLoad(audioSourceSoundGO);
    }

    private void Update()
    {
        if (!audioSourceMusic.isPlaying)
        {
            if (currentIndex == 7)
            {
                currentIndex = 0;
            }

            PlayNext();
        }
    }

    static public void PlayMusic(string title)
    {
        //Recupere l'index associe au titre
        int index = System.Array.IndexOf(lstFilesName, title);
        //Lance le son avec en parametre l'index
        PlayMusic((byte)index);
    }

    static public void PlayMusic(byte index)
    {
        music = Resources.Load<Music>(musicpath + lstFilesName[index]);
        currentIndex = index;
        audioSourceMusic.clip = music.File;
        audioSourceMusic.Play();
    }
    static public void PlayNext()
    {
        //Lance le son suivant
        PlayMusic((byte)(currentIndex + 1));
    }

    static public void PlayPrevious()
    {
        //Lance le son precedent
        PlayMusic((byte)(currentIndex - 1));
    }

    static public void PlayPause()
    {
        //Met en pause le son
        isPaused = !isPaused;
        if (isPaused) audioSourceMusic.Pause();
        else audioSourceMusic.Play();
    }
    static public void PlaySound(byte index)
    {
        audioSourceSound.clip = lstUISounds[index];
        audioSourceSound.Play();
    }
}