using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Character" , menuName = "Characters")]
public class CharacterSettingsSO : ScriptableObject
{
    public string characterName;
    public int age;
    public GameObject idCharacter;
}
