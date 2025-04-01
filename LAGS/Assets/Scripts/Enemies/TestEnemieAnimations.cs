using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class TestEnemieAnimations : MonoBehaviour
{
    [SerializeField] Transform _player;
    private enum EnemysStates { walk,ViewPlayer,Attack, escape, dead };

    [SerializeField] EnemysStates _enemie = EnemysStates.walk;

    int health;
    float velocidadX;
    private Rigidbody rb;
    Animator _anim;

    float currentX;
    private float lastX;

    [SerializeField]float _speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastX = transform.position.x;

        _anim = GetComponent<Animator>();
        health = 10;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_enemie)
        {
            case EnemysStates.walk:

                MovementAnimationEjeX();


                if (health < 5)
                {
                    StartCoroutine(StartViewPlayer());
                    _enemie = EnemysStates.escape;
                }

                

                break;

            case EnemysStates.ViewPlayer:

                FollowPlayer();
                break;

            case EnemysStates.escape:


                if (health < 1)
                {
                    DEAD();
                    _enemie = EnemysStates.dead;
                }

                break;
            case EnemysStates.dead:

                break;
        }

        if (Input.GetKeyDown(KeyCode.S)) health -= 2;
    }

    private void MovementAnimationEjeX()
    {
         currentX = transform.position.x;

        if (currentX > lastX)
        {
            _anim.CrossFade("Right", 0.0001f);
        }
        else if (currentX < lastX)
        {
            _anim.CrossFade("Left", 0.0001f);
        }

        lastX = currentX;
    }

    private void ESCAPE() => _anim.CrossFade("StartBack", 0.0001f);
    

    private void DEAD() => _anim.CrossFade("Dead", 0.0001f);
    

    private IEnumerator StartViewPlayer()
    {
        _anim.CrossFade("StartBack", 0.0001f);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.CrossFade("Back", 0.0001f);
    }

    private bool InRangePlayer2()
    {
        return true;
    
    }

    private void OutOfRangePlayer()
    {
       
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }
}
