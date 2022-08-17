using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : PlayerManager
{

    public Transform fnishCenter;

    [Header("Borders")]
    private float minXBound = -12f;
    private float maxXBound = 12f;

    [Header("Player Settings")]
    public float speed;
    public float swipeSpeed;


    private void Start()
    {
        PlayerManager.Instance.downParticle.Stop();
    }

    #region Movement
    private void FixedUpdate()
    {
         if (GameManager.Instance.gameStart && GameManager.Instance.levelFnish != true)
         {
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime); 

            if (Input.GetMouseButton(0) && !GameManager.Instance.levelFailed)
            {
                Move();
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXBound, maxXBound), transform.position.y, transform.position.z); // Determine the borders for sliding.
        }       
    }

    public void Move() // For sliding.
    {
       
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.localPosition.z;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray,out RaycastHit hit, 150))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = transform.position.y;
            hitPoint.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, hitPoint, Time.deltaTime * swipeSpeed);
        }
    }

    #endregion

    #region Obstacle System
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            GameManager.Instance.currentAge -= 2;
            transform.DOMoveZ(transform.position.z - 10, 0.5f);
            EventManager.CharacterDown(GameManager.Instance.currentAge, PlayerManager.Instance.downParticle);
        }
    }
    #endregion

    #region Fnish System

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Fnish") )
        {
            GameManager.Instance.levelFnish = true;

            transform.DOMove(fnishCenter.position, 1).OnComplete(() =>          // After the character finish the platform he goes to the center of the finish platform then rotate his face to camera.
            {
                transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.WorldAxisAdd);
                GameManager.Instance.fnishCheck();
            });                   
        }
    }

    #endregion
}
