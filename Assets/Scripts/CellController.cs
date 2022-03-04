using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] private Color colorCorrect;
    [SerializeField] private Color colorExist;
    [SerializeField] private Color colorFail;
    [SerializeField] private Color colorNone;
    [SerializeField] private Animator animator;


    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateText(char msg)
    {
        text.SetText(msg.ToString());
    }

    public void UpdateState(State state)
    {
        background.color = GetColor(state);
        
    }

    
    private Color GetColor(State state)
    {
        animator.enabled = true;
        return state switch
        {
            State.None => colorNone,
            State.Contain => colorExist,
            State.Correct => colorCorrect,
            State.Fail => colorFail,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}

public enum State
{
    None,
    Contain,
    Correct,
    Fail
}