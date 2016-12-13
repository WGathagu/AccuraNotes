using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLite;
using System.IO;
using Windows.Storage;

namespace Plain_Notes
{
    public partial class MainPage : PhoneApplicationPage
    {
       
        // The database path.
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "sample.sqlite"));

       
        // The sqlite connection.
        private SQLiteConnection dbConn;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Define the database path. The sqlite database is stored in a file.
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Create the database connection.
            dbConn = new SQLiteConnection(DB_PATH);
            // Create the table Task, if it doesn't exist.
            dbConn.CreateTable<Task>();
            // Retrieve the task list from the database.
            List<Task> retrievedTasks = dbConn.Table<Task>().ToList<Task>();
            // Clear the list box that will show all the tasks.
            TaskListBox.Items.Clear();
            foreach (var t in retrievedTasks)
            {
                TaskListBox.Items.Add(t);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();         // Close the database connection.
        }

        private void Update_List()
        {
            // Retrieve the task list from the database.
            List<Task> retrievedTasks = dbConn.Table<Task>().ToList<Task>();
            // Clear the list box that will show all the tasks.
            TaskListBox.Items.Clear();
            foreach (var t in retrievedTasks)
            {
                TaskListBox.Items.Add(t);
            }
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            // Create a new task.
            Task task = new Task()
            {
                Title = TitleField.Text,
                Text = TextField.Text,
                CreationDate = DateTime.Now
            };
            // Insert the new task in the Task table.
            dbConn.Insert(task);
            Update_List();
        }

        private void Retrieve_Click(object sender, RoutedEventArgs e)
        {
            // Retriving Data 
            var tp = dbConn.Query<Task>("select * from task where title='" + TitleField.Text + "'").FirstOrDefault();
             if (tp == null)
                MessageBox.Show("Title Not Present in DataBase");
            else
                TextField.Text= tp.Text;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // here we updating Notes i.e. Text Filed using Title filed. before that we have to check row present or not
            var testdata = dbConn.Query<Task>("select * from task where title='" + TitleField.Text + "'").FirstOrDefault();
            // Check result is empty or not
            if (testdata == null)
                MessageBox.Show("Title Not Present in DataBase");
            else
            {
                var tp = dbConn.Query<Task>("update task set Text='" + TextField.Text + "' where title = '" + TitleField.Text + "'").FirstOrDefault();
                // Update Database
                dbConn.Update(tp);
                Update_List();
            }
        }

        private  void Delete_Click(object sender, RoutedEventArgs e)
        {
            //  Deleting Entire Row from DB by matching Title Filed
            var tp = dbConn.Query<Task>("select * from task where title='" + TitleField.Text + "'").FirstOrDefault();
            // Check result is empty or not
            if (tp == null)
                MessageBox.Show("Title Not Present in DataBase");
            else
            {
                //Delete row from database
                dbConn.Delete(tp);          //you can delete single column  e.g.    dbConn.Delete(tp.Text);
                Update_List();
            }
        }
    }

   
    // Task class representing the Task table. Each attribute in the class become one attribute in the database.
   
    public sealed class Task
    {
        /// <summary>
        /// You can create an integer primary key and let the SQLite control it.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return Title + ":" + Text + " << " + CreationDate.ToShortDateString() + " " + CreationDate.ToShortTimeString();
        }
    }
}