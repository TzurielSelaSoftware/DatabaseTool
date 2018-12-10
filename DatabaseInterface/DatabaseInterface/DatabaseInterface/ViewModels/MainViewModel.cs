using Wpf;
using System.Windows.Input;
using DatabaseInterface.Models;
using System;
using System.Windows.Forms;
using System.Data;
using System.IO;
using CsvHelper;
namespace DatabaseInterface.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private string searchTable = "Type to Search (First column)";
        public string SearchTable
        {
            get { return searchTable; }
            set
            {
                value = SearchInData(value);
                searchTable = value;
                OnPropertyChanged(nameof(SearchTable));
            }
        }
        private DataTable tableData;
        public DataTable TableData
        {
            get { return tableData; }
            set
            {
                tableData = value;
                OnPropertyChanged(nameof(TableData));
            }
        }
        private String selectedTableName;
        public String SelectedTableName
        {
            get
            {
                return selectedTableName;
            }
            set
            {
                selectedTableName = value;
                this.TableData = this.DbModel.mssqlObj.runTableQuery(selectedTableName);
                ExportToCsvStatus = true;
                OnPropertyChanged(nameof(SelectedTableName));

            }
        }
        private bool exportToCsvStatus;
        public bool ExportToCsvStatus
        {
            get { return exportToCsvStatus; }
            set
            {
                exportToCsvStatus = value;
                OnPropertyChanged(nameof(ExportToCsvStatus));
            }
        }
        private DatabaseModel _dbModel;
        public DatabaseModel DbModel
        {
            get { return _dbModel; }
            set
            {
                if (Equals(_dbModel, value)) return;
                _dbModel = value;
                OnPropertyChanged(nameof(DbModel));
            }
        }
        public CSVModel csvModel { get; set; }
        public ICommand MssqlCommand => new DelegateCommand(MssqlDbConnect);
        public ICommand CsvCommand => new DelegateCommand(ExportToCsv);
        public MainViewModel()
        {
            exportToCsvStatus = false;
        }
        private void MssqlDbConnect()
        {
            DbModel = new DatabaseModel();
        }
        private void ExportToCsv()
        {

            try
            {
                this.csvModel = new CSVModel();
                csvModel.fd = new FolderBrowserDialog();
                if (DialogResult.OK == csvModel.fd.ShowDialog())
                {
                    csvModel.path = csvModel.fd.SelectedPath;
                    SetDataToCSV();
                }
            }
            catch (Exception)
            {
                var msxbxRes = MessageBox.Show("Error exporting to CSV.\nDo you want to try again?", "Error exporting to CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (msxbxRes == DialogResult.Yes)
                {
                    ExportToCsv();
                }
            }
        }
        private void SetDataToCSV()
        {
            try
            {
                var fileName = @"\";
                fileName += DbModel.mssqlObj.tableName + ".csv";
                csvModel.path += fileName;
                using (var tw = new StreamWriter(csvModel.path))
                {
                    using (var csvData = new CsvWriter(tw))
                    {
                        for (int i = 0; i < tableData.Columns.Count; i++)
                        {
                            csvData.WriteField(tableData.Columns[i]);
                        }
                        for (int i = 0; i < tableData.Rows.Count; i++)
                        {
                            csvData.NextRecord();
                            csvData.WriteField(tableData.Rows[i].ItemArray);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string SearchInData(string search)
        {
            var columnToSearch = tableData.Columns[0];
            try
            {
                tableData.DefaultView.RowFilter = string.Format("CONVERT({0}, System.String) LIKE '%{1}%'", columnToSearch, search);
            }
            catch (Exception)
            {
                MessageBox.Show("Error in string format.\nPlease try again.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                SearchTable = "";
            }
            return search;
        }
    }
}
