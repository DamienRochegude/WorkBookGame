using UnityEngine;

public class LoadVerification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading Verification : OK");
        GameManager.Instance.OnGameSceneLoad();
    }
}

