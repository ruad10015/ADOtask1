using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
namespace ADOtask1
{
    public partial class MainWindow : Window
    {
        public string categoryName = "";
       
       
       
        public MainWindow()
        {
            InitializeComponent();
            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;");

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT C.[Name] FROM Categories AS C",sqlConnection) ;
                SqlDataReader reader = sqlCommand.ExecuteReader() ;
             
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        CategoryComboBox.Items.Add(reader[i]);
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryName = CategoryComboBox.Text;
            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;");
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"SELECT B.[Name] FROM Books AS B WHERE B.Id_Category = ANY (SELECT C.Id FROM Categories AS C WHERE C.[Name]='{categoryName}')", sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        BookComboBox.Items.Add(reader[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;");
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(txtbox.Text, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                txtblock.Text = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
                txtblock.Text = "UPDATED | DELETED SUCCESFULLY !";
            }
        }
    }
}