using Microsoft.Data.ConnectionUI;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace DatabaseInterface.Database
{
    class MSSQL
    {
        public string connectionString { get; set; }
        public string dbStatus { get; set; }
        public string color { get; set; }
        public string query { get; set; }
        public string tableName { get; set; }
        public ObservableCollection<string> dbTables { get; set; }
        public DataTable dataSetTableData { get; set; }
        public MSSQL()
        {
            dbDialog();
        }
        private void dbDialog()
        {
            try
            {
                using (var dcd = new DataConnectionDialog())
                {
                    DataSource.AddStandardDataSources(dcd);
                    dcd.SelectedDataSource = DataSource.SqlDataSource;
                    dcd.SelectedDataProvider = DataProvider.SqlDataProvider;
                    var pcName = Environment.MachineName;
                    dcd.ConnectionString = $"Data Source={pcName};Initial Catalog=master;Integrated Security=True";
                    if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
                    {
                        this.connectionString = dcd.ConnectionString;
                        connectToDb(dcd);
                    }
                    else
                    {
                        MessageBox.Show("Button Cancel pressed\nThis program cannot work without DB connection\nProgram will Exit now.", "Dialog Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception)
            {
                var msbx = MessageBox.Show("Connection Dialog Error\nPlease try again\n", "Database connection Dialog Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (DialogResult.OK == msbx)
                {
                    dbDialog();
                }
                else
                {
                    MessageBox.Show("Button Cancel pressed\nThis program cannot work without DB connection\nProgram will Exit now.", "Dialog Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Environment.Exit(1);
                }
            }
        }
        private void connectToDb(DataConnectionDialog dcd)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    dbStatus = connection.State.ToString();
                    if (dbStatus == "Open")
                    {
                        query = $"select *  from {connection.Database}.sys.tables order by name";
                        try
                        {
                            using (var cmd = new SqlCommand(query, connection))
                            {
                                var ds = new DataSet();
                                var da = new SqlDataAdapter(cmd);
                                da.Fill(ds);
                                dbTables = new ObservableCollection<string>();
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    dbTables.Add(row.ItemArray[0].ToString());
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                var msbx = MessageBox.Show("Connection to database faild.\nPlease try again\nHint:\n1.Check that you have access to your Database", "Database connection Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (DialogResult.OK == msbx)
                {
                    connectToDb(dcd);
                }
                else
                {
                    MessageBox.Show("You press cancel\nThis program cannot work without DB connection\nProgram will Exit now.", "Dialog Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Environment.Exit(1);
                }
            }
        }
        public DataTable runTableQuery(string tableName)
        {
            try
            {
                if (tableName != null)
                {
                    this.tableName = tableName;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        query = $"select * from {connection.Database}.dbo.{tableName}";
                        connection.Open();
                        using (var cmd = new SqlCommand(query, connection))
                        {
                            var dt = new DataTable();
                            var da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            dataSetTableData = dt;
                        }
                    }
                }
            }
            catch (Exception)
            {
                var msxbxRes = MessageBox.Show("Error getting table data.\nDo you want to try again?", "Table data Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (msxbxRes == DialogResult.Yes)
                {
                    runTableQuery(this.tableName);
                }
            }
            return dataSetTableData;
        }
    }
}
