using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;
    public static LoadingScreen Instance{get=>instance;}
    [SerializeField] private Sprite[] Poster;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text progressUI;
    private void Awake() => gameObject.SetActive(false);
    void Start()
    {
        SetImage();
        StartCoroutine(ProgessBar());        
    }
    private void SetImage()
    {
        SpawnManager.Instance.Choosing.SetActive(false);
        string dinoname=PlayerPrefs.GetString("dino");
        foreach (var item in Poster)
        {
            if(item.name.ToString().Contains(dinoname))
                gameObject.GetComponent<Image>().sprite=item;
        }
    }
    private IEnumerator ProgessBar()
    {
        var scene = SceneManager.LoadSceneAsync("Game");
        while (!scene.isDone)
        {
            float progress =Mathf.Clamp01(scene.progress/.9f);
            loadingBar.value=progress;
            progressUI.text=progress*100f+"%";
            yield return null;
        }
    }
}
