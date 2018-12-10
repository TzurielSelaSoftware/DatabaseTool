using DatabaseInterface.Database;
using System.Collections.ObjectModel;
namespace DatabaseInterface.Models
{
    class DatabaseModel
    {
        public MSSQL mssqlObj { get; set; }
        public string dbStatus { get; set; }
        public bool color { get; set; }
        public ObservableCollection<string> dbTables { get; set; }
        public DatabaseModel()
        {
            mssqlObj = new MSSQL();
            this.dbStatus = mssqlObj.dbStatus;
            if (this.dbStatus == "Open")
                this.color = true;
            this.dbTables = mssqlObj.dbTables;
        }
    }
}
