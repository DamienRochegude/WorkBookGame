using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Music")]
public class Music : ScriptableObject
{
    [SerializeField] string title;
    [SerializeField] string author;
    [SerializeField] AudioClip file;
    [SerializeField] string description;

    public string Title { get => title; }
    public string Author { get => author; }
    public AudioClip File { get => file; }
    public string Description { get => description; }
}