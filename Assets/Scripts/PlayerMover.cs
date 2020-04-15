using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    IPlayerInput playerInput;
    Rigidbody2D Rb2D;
    [SerializeField] float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        Rb2D = GetComponent<Rigidbody2D>();
        playerInput.MoveDirectionReactiveProperty.Skip(1).Subscribe(x => Rb2D.velocity = x * playerSpeed).AddTo(gameObject);
        playerInput.OnActionButtonObservable.Skip(1).Where(flag => flag == true).Subscribe(flag => PlayerAction()).AddTo(gameObject);
        playerInput.OnChangeButtonObservable.Skip(1).Where(flag => flag == true).Subscribe(flag => ChangeCharacter()).AddTo(gameObject);
    }


    void PlayerAction()
    {
        Debug.Log("Action");
    }
    void ChangeCharacter()
    {
        Debug.Log("Change");
    }
}
