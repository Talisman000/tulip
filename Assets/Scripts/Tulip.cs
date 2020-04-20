using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tulip : MonoBehaviour
{
    public CharacterInfo characterInfo;
    BoxCollider2D boxCollider2D;
    [SerializeField] int points;
    [SerializeField] float addPointsInterval;
    AudioSource audioSource;
    [SerializeField] AudioClip popSE;
    private void Start()
    {
        transform.DOPunchScale(Vector2.one * 0.3f, 0.3f, 1);
        GameManager.tulipBloomNumbers[(int)characterInfo.characterType]++;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(popSE);
        StartCoroutine(AddPoints());
    }

    public IEnumerator DestroyCoroutine(){
        GameManager.tulipBloomNumbers[(int)characterInfo.characterType]--;
        gameObject.tag = "Untagged";
        transform.DOScale(Vector2.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        yield break;
    }

    IEnumerator AddPoints(){
        while(true){
            yield return new WaitForSeconds(addPointsInterval);
            if(GameManager.isGame.Value) GameManager.score += points;
        }
    }
}
