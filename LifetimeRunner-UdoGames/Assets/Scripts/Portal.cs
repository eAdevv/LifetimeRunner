using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Portal : MonoBehaviour
{
   public enum conditionState
   {
        add,
        multiplier,
   }
   
    public conditionState currentState;
    public int size;

    private bool isGateActive = true;
    private MeshRenderer meshRenderer;
    private TextMeshPro sizeText;
    private ParticleSystem characterChangeParticle;
   
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sizeText = GetComponentInChildren<TextMeshPro>();
        characterChangeParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
       characterChangeParticle.Stop();
       switch (currentState) // Changing gate text .
        {
            case conditionState.add:
                sizeText.text = "+" + size.ToString();
                break;         
            case conditionState.multiplier:
                sizeText.text = "x" + size.ToString();
                break;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isGateActive)
        {
            meshRenderer.enabled = false;
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false; // Closing number Text
            gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false; // Closing year Text

            StartCoroutine(GateActive()); 

            switch (currentState) // We choose the gate property for its function ( + or x ).
            {
                case conditionState.add:
                    GameManager.Instance.currentAge += size;
                    EventManager.Character_Update(GameManager.Instance.currentAge , characterChangeParticle); // Calling update function for change character.
                    break;
                case conditionState.multiplier:
                    GameManager.Instance.currentAge *= size;
                    EventManager.Character_Update(GameManager.Instance.currentAge , characterChangeParticle);
                    break;
            }
        }
    }
    public IEnumerator GateActive()
    {
        isGateActive = false;
        yield return new WaitForSeconds(1.7f);
        isGateActive = true;
    }


}
