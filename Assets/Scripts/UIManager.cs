using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform title;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] PlayerStateController playerStateController;
    [SerializeField] Slider HPbar;
    RectTransform hpRect;
    [SerializeField] Slider Timebar;
    RectTransform timeRect;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Gradient fieldgradient;
    [SerializeField] RectTransform result;
    [SerializeField] TextMeshProUGUI resultScore;
    [SerializeField] TextMeshProUGUI totalTulip;
    [SerializeField] TextMeshProUGUI detailTulip;

    [SerializeField] float duration;
    [SerializeField] int resultBaibaiTime = 60;
    int resultBaibaiTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.isGame.Skip(1).Subscribe(flag =>
        {
            if (flag) GameUIEnter(duration);
            else
            {
                GameUIExit(duration);
                ResultUIEnter(duration);
            }
        });
        playerStateController.HP.Subscribe(hp => DOVirtual.Float(HPbar.value, (float)hp / 100, 0.5f, value => HPbar.value = value));
        hpRect = HPbar.GetComponent<RectTransform>();
        timeRect = Timebar.GetComponent<RectTransform>();
        highscore.text = string.Format("HighScore\t{0}", GameManager.highScore);
        GameUIExit(0);
        result.DOLocalMoveX(600, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Timebar.value = GameManager.gameTimer / GameManager.maxGameTimer;
        background.color = fieldgradient.Evaluate(GameManager.gameTimer / GameManager.maxGameTimer);
        score.text = GameManager.score.ToString();
    }
    void GameUIEnter(float duration)
    {
        title.DOLocalMoveY(400, duration).SetEase(Ease.InQuad);
        score.rectTransform.parent.DOLocalMoveX(338.68f, duration);
        hpRect.DOLocalMoveX(-201.62f, duration);
        timeRect.DOLocalMoveX(-371.51f, duration);
    }
    void GameUIExit(float duration)
    {
        score.rectTransform.parent.DOLocalMoveX(600f, duration).SetEase(Ease.InQuad);
        hpRect.DOLocalMoveX(-500f, duration).SetEase(Ease.InQuad);
        timeRect.DOLocalMoveX(-500f, duration).SetEase(Ease.InQuad);
    }

    void ResultUIEnter(float duration)
    {
        resultScore.text = string.Format("Score {0,6:d}", GameManager.score);
        int total = 0;
        string detail = "";
        for (int i = 0; i < GameManager.tulipBloomNumbers.Length; i++)
        {
            total += GameManager.tulipBloomNumbers[i];
            detail += string.Format("{0} {1,4:d}\n", (CharacterType)Enum.ToObject(typeof(CharacterType), i), GameManager.tulipBloomNumbers[i]);
        }
        totalTulip.text = string.Format("Tulip {0,4:d}", total);
        detailTulip.text = detail;
        result.DOLocalMoveX(300, duration);
        StartCoroutine(ResultBaibaiCoroutine(duration));
    }

    IEnumerator ResultBaibaiCoroutine(float duration)
    {
        resultBaibaiTimer = 0;
        while (true)
        {
            if (resultBaibaiTimer > resultBaibaiTime)
            {
                result.DOLocalMoveX(600, duration).SetEase(Ease.InQuad);
                yield break;
            }
            resultBaibaiTimer++;

            yield return new WaitForSeconds(1);
        }
    }
}
