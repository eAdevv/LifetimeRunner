using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    public CharacterSettingsSO characterSettings;
    public string characterName;
    public int characterAge;

    private void Start()
    {
        characterName = characterSettings.characterName;
        characterAge = characterSettings.age;
    }

}
