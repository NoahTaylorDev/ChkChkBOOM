using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Scriptable Objects/ObjectData")]
public class ObjectData : ScriptableObject
{
    [Header("HP")]
    public float health = 1;
    
    [Header("Weight")]
    [Tooltip("The amount of Drag and if the player can push the object or not")]
    public float mass = 1f;
    public float linearDamping = 0.5f;
    public float angularDamping = 0.5f;
    
    [Header("Visuals")]
    public Sprite objectSprite;
    public Sprite mediumDamageSprite;
    public Sprite heavyDamageSprite;
}
