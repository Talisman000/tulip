using UnityEngine;
using UniRx;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterInfo")]
public class CharacterInfo:ScriptableObject{
    public CharacterType characterType;
    public float recoverHP;
    public ToolsController toolsController;
}
public enum CharacterType{
    Red,
    White,
    Yellow
}