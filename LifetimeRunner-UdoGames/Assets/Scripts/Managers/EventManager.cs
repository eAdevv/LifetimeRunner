using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager 
{

    public static event TestEventDelegate OnCharacterUp;
    public static event TestEventDelegate OnCharacterDown;
    public delegate void TestEventDelegate(int size , ParticleSystem particle); 

    public static void Character_Update(int myAge , ParticleSystem myParticle) 
    {
        OnCharacterUp?.Invoke(myAge,myParticle);
    }
    public static void CharacterDown(int myAge, ParticleSystem myParticle)
    {
        OnCharacterDown?.Invoke(myAge, myParticle);
    }



}
