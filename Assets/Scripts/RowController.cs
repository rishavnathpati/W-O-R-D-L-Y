using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    [SerializeField] private List<CellController> cells;
    public int CellAmount => cells.Count;
    
    public void UpdateText(string msg)
    {
        var arrayChar = msg.ToCharArray();
        for (var i = 0; i < cells.Count; i++)
        {
            var cell = cells[i];

            var isExist = i < arrayChar.Length;
            var content = isExist ? arrayChar[i] : ' ';
            cell.UpdateText(content);
        }
    }

    public void UpdateState(List<State> states)
    {
        StartCoroutine(RevealCellColours(states));
    }

    private IEnumerator RevealCellColours(IReadOnlyList<State> states)
    {
        for (var i = 0; i < states.Count; i++)
        {
            var cell = cells[i];
            var state = states[i];
            cell.UpdateState(state);
            yield return new WaitForSeconds(0.3f);
        }
    }
    
}