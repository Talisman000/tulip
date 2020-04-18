using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    private static SceneTransition instance;
    private bool isSceneChange = false;
    [SerializeField] GameObject spriteMask;
    [SerializeField] float duration;
    public static SceneTransition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate((GameObject)Resources.Load("Prefabs/SceneTransition")).GetComponent<SceneTransition>();
            }
            return instance;
        }
    }

    private void Start() {
        spriteMask.transform.DOScale(Vector2.one * 1.4f,0f);
        DontDestroyOnLoad(this);
    }
    public void ChangeScene(string sceneName){
        if(isSceneChange) return;
        isSceneChange = true;
        StartCoroutine(ChangeSceneCoroutine(sceneName));
    }

    IEnumerator ChangeSceneCoroutine(string sceneName){
        spriteMask.transform.DOScale(Vector2.zero,duration);
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
        spriteMask.transform.DOScale(Vector2.one * 1.4f,duration);
        yield return new WaitForSeconds(duration);
        isSceneChange = false;
        yield return null;
    }
}
