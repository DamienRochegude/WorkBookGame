using UnityEngine;

public class LevelManager : GenericSingletonClass<LevelManager>
{
    private const string levelpath = "Levels/";
    private static string[] levelFilesName;
    public static string[] LevelFilesName { set { levelFilesName = value; } }


    private static GameObject[] lstGOPage = new GameObject[4];

    public static string parent2DName;
    private static Transform[] transfParent2D = new Transform[4];
    public static Transform[] TransfParent2D { set { transfParent2D = value; } }

    private static int playerLevelIndex = 0;
    private static Level currentLevel = null;

    private static Vector3 posCurrent;

    public void LoadLevel()
    {
        if (levelFilesName.Length < 4)
        {
            Debug.LogError("Level count : " + levelFilesName.Length);
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            transfParent2D[i] = GameObject.Find(parent2DName).transform.GetChild(i);
            GameObject curLevel = Resources.Load<Level>(levelpath + levelFilesName[i]).LevelPrefab;
            Transform currentTransf = null;
            for (int j = 0; j < transfParent2D[i].childCount; j++)
            {
                if (transfParent2D[i].GetChild(j).gameObject.layer == 8)
                {
                    currentTransf = transfParent2D[i].GetChild(j);
                    break;
                }
            }
            if (currentTransf != null)
            {
                Destroy(currentTransf.gameObject);
            }
            lstGOPage[i] = Instantiate(curLevel, transfParent2D[i]);
        }
        currentLevel = Resources.Load<Level>(levelpath + levelFilesName[0]);
        PlayerManager.Spawn(currentLevel.PositionSpawn);
    }

    // remplaceLevel(0,3) Level3 in L
    public static void remplaceLevel(byte indexPage, int indexLevel)
    {
        if (indexLevel >= levelFilesName.Length || indexLevel < 0 || indexPage > 3 || indexPage < 0)
            return;
        Destroy(lstGOPage[indexPage]);
        Level level = Resources.Load<Level>(levelpath + levelFilesName[indexLevel]);
        lstGOPage[indexPage] = Instantiate(level.LevelPrefab, transfParent2D[indexPage]);
    }

    public void NextLevel()
    {
        PlayerManager.DesroyPlayer();
        if (playerLevelIndex + 1 >= levelFilesName.Length)
        {
            Debug.LogError("Next Level Doesn't exist");
            return;
        }

        Debug.Log("Player : " + playerLevelIndex + " to " + (playerLevelIndex + 1));

        playerLevelIndex++;

        if (playerLevelIndex % 2 == 0)
        {
            remplaceLevel(0, playerLevelIndex - 2);
            remplaceLevel(1, playerLevelIndex + 1);
            remplaceLevel(2, playerLevelIndex - 1);
            remplaceLevel(3, playerLevelIndex);
            AnimationManager.Instance.turnLeftPage();
        }
        currentLevel = Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex]);
        PlayerManager.Spawn(currentLevel.PositionSpawn);
    }

    public void NextLevel(Vector3 pos)
    {

        Debug.Log("Spawn to : " + pos);
        PlayerManager.DesroyPlayer();
        if (playerLevelIndex + 1 >= levelFilesName.Length)
        {
            Debug.LogError("Next Level Doesn't exist");
            return;
        }

        Debug.Log("Player : " + playerLevelIndex + " to " + (playerLevelIndex + 1));

        playerLevelIndex++;

        if (playerLevelIndex % 2 == 0)
        {
            remplaceLevel(0, playerLevelIndex - 2);
            remplaceLevel(1, playerLevelIndex + 1);
            remplaceLevel(2, playerLevelIndex - 1);
            remplaceLevel(3, playerLevelIndex);
            posCurrent = pos;
            AnimationManager.Instance.turnLeftPage();
        }
        else
        {
            PlayerManager.Spawn(pos);
        }

        currentLevel = Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex]);
    }

    public static void LoadNextLevel()
    {
        remplaceLevel(0, playerLevelIndex);
        remplaceLevel(1, playerLevelIndex + 1);
        PlayerManager.Spawn(posCurrent);
    }

    public void PreviousLevel()
    {
        PlayerManager.DesroyPlayer();

        if (playerLevelIndex - 1 < 0)
        {
            Debug.LogError("Previous Level Doesn't exist");
            return;
        }

        Debug.Log("Player : " + playerLevelIndex + " to " + (playerLevelIndex - 1));

        playerLevelIndex--;

        if (playerLevelIndex % 2 != 0)
        {
            remplaceLevel(0, playerLevelIndex - 1);
            remplaceLevel(1, playerLevelIndex + 2);
            remplaceLevel(2, playerLevelIndex);
            remplaceLevel(3, playerLevelIndex + 1);
            AnimationManager.Instance.turnRightPage();
        }
        currentLevel = Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex]);

        PlayerManager.Spawn(currentLevel.PositionSpawn);

    }

    public void PreviousLevel(Vector3 pos)
    {
        PlayerManager.DesroyPlayer();

        if (playerLevelIndex - 1 < 0)
        {
            Debug.LogError("Previous Level Doesn't exist");
            return;
        }

        Debug.Log("Player : " + playerLevelIndex + " to " + (playerLevelIndex - 1));

        playerLevelIndex--;

        if (playerLevelIndex % 2 != 0)
        {
            remplaceLevel(0, playerLevelIndex - 1);
            remplaceLevel(1, playerLevelIndex + 2);
            remplaceLevel(2, playerLevelIndex);
            remplaceLevel(3, playerLevelIndex + 1);
            posCurrent = pos;
            AnimationManager.Instance.turnRightPage();
        }
        else
        {
            PlayerManager.Spawn(pos);
        }
        currentLevel = Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex]);


    }

    public static void LoadPreviousLevel()
    {
        remplaceLevel(0, playerLevelIndex - 1);
        remplaceLevel(1, playerLevelIndex);
        PlayerManager.Spawn(posCurrent);
    }

    public static Vector3 GetPosSpawnCurLevel()
    {
        return currentLevel.PositionSpawn;
    }

    public static Vector3 GetPosSpawnNextLevel()
    {

        if (playerLevelIndex + 1 >= levelFilesName.Length)
        {
            Debug.LogError("Next Level Doesn't exist");
            return Vector3.zero;
        }

        return Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex + 1]).PositionSpawn;
    }

    public static Vector3 GetPosSpawnPreviousLevel()
    {

        if (playerLevelIndex - 1 < 0)
        {
            Debug.LogError("Previous Level Doesn't exist");
            return Vector3.zero;
        }

        return Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex - 1]).PositionSpawn;
    }


    public static Transform GetCurrentLevel()
    {
            return lstGOPage[playerLevelIndex % 2].transform;
    }

    public static void RestartPlayerLevel()
    {
        playerLevelIndex -= playerLevelIndex % 2;
        currentLevel = Resources.Load<Level>(levelpath + levelFilesName[playerLevelIndex]); 
    }
}