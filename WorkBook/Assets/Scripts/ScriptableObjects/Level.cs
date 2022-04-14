using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    private string levelDescription;
    [SerializeField]
    private GameObject levelPrefab;
    [SerializeField]
    private Vector3 positionSpawn;

    public string LevelDescription { get { return levelDescription; } }
    public GameObject LevelPrefab { get { return levelPrefab; } }
    public Vector3 PositionSpawn { get { return positionSpawn; } }

}