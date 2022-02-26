using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace CRUDEmployee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }
        
        public void clearData()
        {
            inp_Name.Clear();
            inp_Age.Clear();
            inp_Phone.Clear();
            inp_Salary.Clear();
            inp_Date.Clear();
            inp_ID.Clear();
        }

        public bool isValid()
        {
            if (inp_Name.Text == string.Empty)
            {
                MessageBox.Show("Name is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (inp_Age.Text == string.Empty)
            {
                MessageBox.Show("Age is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (inp_Phone.Text == string.Empty)
            {
                MessageBox.Show("Phone is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (inp_Salary.Text == string.Empty)
            {
                MessageBox.Show("Salary is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (inp_Date.Text == string.Empty)
            {
                MessageBox.Show("Date is Required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                try
                {

                    string query = "insert into cruddb.employees(Name,Age,Phone,Salary,Date)  values('" + this.inp_Name.Text + "','" + this.inp_Age.Text + "', '" + this.inp_Phone.Text + "', '" + this.inp_Salary.Text + "', '" + this.inp_Date.Text + "');";

                    MySqlCommand command = new MySqlCommand(query, conn);
                    MySqlDataReader reader;
                    conn.Open();
                    reader = command.ExecuteReader();                 
                    while (reader.Read())
                    {

                    }
                    conn.Close();
                    LoadGrid();
                    MessageBox.Show("Data Saved!");
                    clearData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }         
        }

        public void LoadGrid()
        {
           
            MySqlCommand cmd = new MySqlCommand("select * from cruddb.employees", conn);
            DataTable dt = new DataTable();
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            conn.Close();
            datagridtable.ItemsSource = dt.DefaultView;
        }
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("update cruddb.employees set Name = '" + this.inp_Name.Text + "',  Age = '" + this.inp_Age.Text + "', Phone = '" + this.inp_Phone.Text + "', Salary = '" + this.inp_Salary.Text + "', Date = '" + this.inp_Date.Text + "' where ID = '" + this.inp_ID.Text + "';", conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated!", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                clearData();
                LoadGrid();
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from cruddb.employees where ID = '" + this.inp_ID.Text + "';", conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                clearData();
                LoadGrid();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
    }
}
