using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    PlayerMover playerMover;
    PlayerActionHandler playerActionHandler;
    Animator animator;
    bool isAnimatorMove{
        set{
            animator.SetBool("isMove",value);
        }
    }

    int characterSkin{
        set{
            animator.SetInteger("skin",value);
        }
    }

    bool isSleep{
        set{
            animator.SetBool("isSleep",value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMover = GetComponent<PlayerMover>();
        playerActionHandler = GetComponent<PlayerActionHandler>();
        playerMover.isMove.Subscribe(x => isAnimatorMove = x).AddTo(gameObject);
        playerActionHandler.ChangeCharacterObservable.Subscribe(info => characterSkin = (int)info.characterType);
        GameManager.isGame.Subscribe(flag => isSleep = !flag);
    }
}
