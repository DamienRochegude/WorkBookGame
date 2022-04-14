using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : GenericSingletonClass<SManager>
{
    private static int sceneGameIndex;
    private static Scene gameScene;

    public static int SceneGameIndex
    {
        get { return sceneGameIndex; }
        set
        {
            if (value < SceneManager.sceneCountInBuildSettings)
            {
                sceneGameIndex = value;
            }
            else
            {
                Debug.LogError("Index : " + value + " don't exist !");
            }
        }
    }
    public void LoadSceneGame()
    {
        Debug.Log("Changing to Game Scene");
        SceneManager.LoadScene(SceneGameIndex);
    }

    public Scene GetGameScene()
    {
        gameScene = SceneManager.GetActiveScene();
        return gameScene;
    }
}
