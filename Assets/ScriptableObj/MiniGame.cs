using UnityEngine;

[CreateAssetMenu(fileName = "MiniGame", menuName = "Scriptable Objects/MiniGame")]
public class MiniGame : ScriptableObject
{
    public string SceneName;
    public int weight;
    public bool CanAppear;
    public int CurrentDownTime;
}
