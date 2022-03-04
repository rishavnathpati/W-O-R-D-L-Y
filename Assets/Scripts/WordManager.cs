using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] public string origin;
    
    private void Start()
    {
        var randomWord = WordList.Words[Random.Range(0, WordList.Words.Count)];
        origin = randomWord;
    }
    
    public List<State> GetStates(string msg)
    {
        var result = new List<State>();
        var list = origin.ToUpper().ToCharArray().ToList();
        var listCurrent = msg.ToCharArray().ToList();
        foreach (var variable in listCurrent) Debug.Log(variable);

        for (var i = 0; i < listCurrent.Count; i++)
        {
            var currentChar = listCurrent[i];
            var contains = list.Contains(currentChar);
            if (contains)
            {
                var index = list.FindIndex(x => x == currentChar);
                var isCorrect = index == i;
                result.Add(isCorrect ? State.Correct : State.Contain);
                list[index] = '.';
            }
            else
            {
                result.Add(State.Fail);
            }
        }

        return result;
    }
}