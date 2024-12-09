using Assets._Project.Scripts.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._project.CodeBase
{
    public class MenuControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _newRecordPanel;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private RecordsController _recordsController;
        [SerializeField] private RecordView _recordView;

        public void ShowNewRecordPanel(int playerScore)
        {
            _newRecordPanel.SetActive(true);

            void OnSaveNewRecord()
            {
                string playerName = _nameInputField.text;

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    playerName = "Игрок"; // Если поле ввода пустое, задаем имя по умолчанию
                }

                _recordsController.AddNewRecord(playerName, playerScore);
                _recordView.UpdateRecords(_recordsController.HighScores);
                _newRecordPanel.SetActive(false);
            }

            // Привязка кнопки к методу
            Button saveButton = _newRecordPanel.GetComponentInChildren<Button>();
            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(OnSaveNewRecord);
        }
    }
}

