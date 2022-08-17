using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public PlayerController PlayerController { get; private set; }

    public CharacterUpdate CharacterUpdate { get; private set; }

    public CharacterDisplay CharacterDisplay { get; private set; }

    public ParticleSystem downParticle { get; private set; }

    public Rigidbody Rigidbody{ get; private set; }

    public Animator Animator { get; private set; }



    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();

        CharacterUpdate = GetComponent<CharacterUpdate>();

        CharacterDisplay = GetComponentInChildren<CharacterDisplay>();

        downParticle = GetComponentInChildren<ParticleSystem>();

        Rigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        Animator = GetComponentInChildren<Animator>();
    }


}
