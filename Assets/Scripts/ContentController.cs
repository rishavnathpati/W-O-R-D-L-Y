using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button temp;
    [SerializeField] private List<RowController> rows;
    [SerializeField] private WordManager wordManager;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject keyboard;
    [SerializeField] private GameObject inputController;
    [SerializeField] private GameObject gameOverPanel;

    private CanvasGroup _gameOverPanelCanvasGroup;
    private int _index;

    private void Start()
    {
        _gameOverPanelCanvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
        inputField.onValueChanged.AddListener(OnUpdateContent);
        inputField.onSubmit.AddListener(OnSubmit);

        switch (SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                inputController.SetActive(false);
                keyboard.SetActive(true);
                break;
            case DeviceType.Desktop:
                inputController.SetActive(true);
                keyboard.SetActive(false);
                break;
            case DeviceType.Unknown:
                break;
            case DeviceType.Console:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnUpdateContent(string msg)
    {
        var row = rows[_index];
        if (msg.ToCharArray().Length >= 5)
            msg = msg.Substring(0, 5);

        Debug.Log(msg);
        row.UpdateText(msg);
    }

    private bool UpdateState()
    {
        var stateList = wordManager.GetStates(inputField.text.ToUpper());
        rows[_index].UpdateState(stateList);

        return stateList.All(state => state == State.Correct);
    }

    public void OnSubmit(string msg)
    {
        temp.Select();
        inputField.Select();

        if (!IsValid())
        {
            Debug.Log("Insufficient");
            return;
        }

        var isWin = UpdateState();
        if (isWin)
        {
            GameOver("Win");
            inputField.enabled = false;
            return;
        }

        _index++;
        var isLost = _index == rows.Count;
        if (isLost)
        {
            GameOver("Lose");
            inputField.enabled = false;
            return;
        }

        inputField.text = "";
    }

    private void GameOver(string msg)
    {
        resultText.text = wordManager.origin.ToUpper();

        resultText.text += msg switch
        {
            "Win" => "\n\nYou Win!!" + "\nPlay again",
            "Lose" => "\n\nYou Lose" + "\nPlay again",
            _ => resultText.text
        };

        gameOverPanel.SetActive(true);
        StartCoroutine(DoFade(0, 1, 1));
        inputController.SetActive(false);
    }

    private IEnumerator DoFade(float startAlphaValue, float endAlphaValue, float duration)
    {
        var counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            _gameOverPanelCanvasGroup.alpha = Mathf.Lerp(startAlphaValue, endAlphaValue, counter / duration);

            yield return null;
        }
    }

    public void PlayAgainYes()
    {
        StartCoroutine(DoFade(1, 0, 1));
        SceneManager.LoadScene(0);
    }

    public void PlayAgainNo()
    {
        Application.Quit();
    }

    private bool IsValid()
    {
        return (WordList.Words.Contains(inputField.text.ToLower()) ||
                WordList.AcceptableWords.Contains(inputField.text.ToLower())) &&
               inputField.text.Length == rows[_index].CellAmount;
    }
}