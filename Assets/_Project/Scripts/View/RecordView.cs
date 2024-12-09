using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> _playerNames;
        [SerializeField] private List<TextMeshProUGUI> _playerScores;

        public void UpdateRecords(HighScores highScores)
        {
            for (int i = 0; i < _playerNames.Count; i++)
            {
                if (i < highScores.Records.Count)
                {
                    _playerNames[i].text = highScores.Records[i].PlayerName;
                    _playerScores[i].text = highScores.Records[i].Score.ToString();
                }
                else
                {
                    _playerNames[i].text = "Нет данных";
                    _playerScores[i].text = "0";
                }
            }
        }
    }
}
