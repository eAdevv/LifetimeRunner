using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CharacterUpdate : PlayerManager 
{

    public List<GameObject> characters = new List<GameObject>(); // We hold characters in the Player Base .
    public GameObject currentCharacter; // To simply change the character, it is necessary to determine what is active at each change.

    private void Start()
    {
        currentCharacter = characters[0];
    }


    private void OnEnable() 
    {
        EventManager.OnCharacterUp += characterUp;
        EventManager.OnCharacterDown += characterDown;
    }
    private void OnDisable() 
    {
        EventManager.OnCharacterUp -= characterUp;
        EventManager.OnCharacterDown -= characterDown;    
    }


    #region UPDATING FUNCTIONS
    public void characterUp(int myAge , ParticleSystem myParticle) // We should check the ages for UP the character then we have to close the previous character.
    {
        for (int i = 1; i <= characters.Count - 1; i++) // We're checking all of them as the character's age can get too high at a single door like 0 to 8 or 2 to 14 etc. .
        {
            if (characters[i].GetComponent<CharacterDisplay>().characterAge <= myAge)
            {
                var tempCharacter = currentCharacter;

                myParticle.Play();
                characters[i].SetActive(true);
                characters[i].transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.WorldAxisAdd);
                currentCharacter = characters[i];
                tempCharacter.SetActive(false);
                SetAgeText();
            }
        }
    }

    public void characterDown(int myAge, ParticleSystem myParticle) 
    {

        if (currentCharacter.GetComponent<CharacterDisplay>().characterAge > myAge) // When current age is less than active character age we go back to previous character .
        {
            var holdIndex = characters.FindIndex(c => c == currentCharacter);
            myParticle.Play();
            characters[holdIndex - 1].SetActive(true); 
            currentCharacter.SetActive(false);
            currentCharacter = characters[holdIndex - 1];
        }
    }

    #endregion
    public void SetAgeText()
    {
        currentCharacter.GetComponentInChildren<TextMeshPro>().text = GameManager.Instance.currentAge.ToString();
    }


}




