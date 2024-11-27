using System.IO;

namespace Assets._Project.Scripts
{
    public class RecordsController
    {
        private readonly RecordsView view;
        private readonly string recordsFilePath = "records.txt";

        public RecordsController(RecordsView view)
        {
            this.view = view;
            DisplayRecords();
        }

        private void DisplayRecords()
        {
            if (File.Exists(recordsFilePath))
            {
                var records = File.ReadAllText(recordsFilePath);
                view.DisplayRecords(records);
            }
            else
            {
                view.DisplayRecords("No records found.");
            }
        }
    }
}
