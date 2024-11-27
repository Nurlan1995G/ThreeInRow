using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts
{
    public class RecordsView : MonoBehaviour
    {
        public Text RecordsText;

        public void DisplayRecords(string records)
        {
            RecordsText.text = records;
        }
    }
}
