﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Title;
    [SerializeField] string levelName;
    [SerializeField] Image fadeScreen;

    [SerializeField] Candle candle;

    void Awake() {
        if (Title == null) {
            Title = this;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(StartGame());
        }

        
    }

    IEnumerator StartGame() {
        yield return StartCoroutine(DoBreath());
        yield return StartCoroutine(FadeOut());
        yield return StartCoroutine(LoadLevel());
    }

    IEnumerator DoBreath() {
        candle.flame.SetActive(false);
        yield break;
    }

    IEnumerator FadeOut() {
        float fadeOutTime = 2f;
        float currentTime = 0f;

        if (fadeScreen == null) {
            yield return new WaitForSeconds(fadeOutTime);
            yield break;
        }

        Color startColor = fadeScreen.color;
        Color endColor = startColor;
        endColor.a = 1;
        Color currentColor = startColor;
        while (currentTime < fadeOutTime) {
            currentTime += Time.deltaTime;
            currentColor = Color.Lerp(startColor, endColor, currentTime/fadeOutTime);
            fadeScreen.color = currentColor;
            yield return null;
        }
    }

    IEnumerator LoadLevel() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }

}

