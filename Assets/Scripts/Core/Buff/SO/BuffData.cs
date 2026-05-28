using UnityEngine;
[CreateAssetMenu(menuName = "Bomberman/BuffData")]
public class BuffData : ScriptableObject
{
    public KeyPool buffKey;
    public float duration;
    public BuffType type;
    public float value;
}
