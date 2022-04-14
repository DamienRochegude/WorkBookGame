using UnityEngine;
using UnityEngine.Audio;

public class GameManager : GenericSingletonClass<GameManager>
{
    private static SManager sceneManager;
    private static SoundManager soundManager;
    private static PlayerManager playerManager;
    private static LevelManager levelManager;
    private static UIManager uiManager;
    private static AnimationManager animationManager;
    private static CameraManager cameraManager;

    [Header("SceneManager Settings")]
    public int indexSceneGame = 1;


    [Header("SoundManager Settings")]
    public AudioMixerGroup audioMixer;
    public string[] lstFilesSoundName;
    public AudioClip[] lstUISounds;

    [Header("Player Settings")]
    public GameObject player;
    public Vector3 initPos;
    public float moveSpeed;
    public float jumpVelocity;
    public float dashSpeed;
    public float dashDistance;
    public string playerPosGOName;

    [Header("LevelManager Settings")]
    public string[] levelFilesName;
    public string parent2DName;
    public string[] fileNameBridges;

    [Header("AnimationManager Settings")]
    public string animatorPageGameObjectName;
    public AnimationClip animationClip;

    [Header("CameraManager Settings")]
    public GameObject[] lstGOCameras;
    public string parentCameraName;

    // Test
    public bool test1 = false;
    public bool test2 = false;

    private void Start()
    {
        sceneManager = SManager.Instance;
        soundManager = SoundManager.Instance;
        playerManager = PlayerManager.Instance;
        levelManager = LevelManager.Instance;
        uiManager = UIManager.Instance;
        animationManager = AnimationManager.Instance;
        cameraManager = CameraManager.Instance;

        SManager.SceneGameIndex = indexSceneGame;
        SManager.Instance.LoadSceneGame();

        SoundManager.AudioMixer = audioMixer;
        SoundManager.LstFilesName = lstFilesSoundName;
        SoundManager.LstUISounds = lstUISounds;

        LevelManager.LevelFilesName = levelFilesName;
        LevelManager.parent2DName = parent2DName;

        AnimationManager.animatorPageGameObjectName = animatorPageGameObjectName;
        AnimationManager.animationClip = animationClip;

        PlayerManager.PlayerPrefab = player;
        PlayerManager.MoveSpeed = moveSpeed;
        PlayerManager.JumpVelocity = jumpVelocity;
        PlayerManager.DashSpeed = dashSpeed;
        PlayerManager.DashDistance = dashDistance;

        CameraManager.LstGOCameras = lstGOCameras;
        CameraManager.ParentName = parentCameraName;
    }

    public void OnGameSceneLoad()
    {
        levelManager.LoadLevel();
        animationManager.SetAnimator();
        cameraManager.InitCameras();  
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    private void Update()
    {
        if (test1)
        {
            // Do something
            levelManager.NextLevel();
            test1 = false;
        }

        if (test2)
        {
            PlayerManager.Instance.RestartLevel();
            test2 = false;
        }
    }
}