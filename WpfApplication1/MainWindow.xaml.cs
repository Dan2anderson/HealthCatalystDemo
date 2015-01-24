/// Author: Daniel Anderson  Jan.23,2015
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //I used the DbImporter class  to import the data from the data.csv file to the database. 
            //Just creat an instance of DbImporter and call the FillDb() method
           
        }

        //search button click action
        public void SearchClick(object sender, RoutedEventArgs e)
        {
            MatchingStack.Children.Clear();
            using (MemberInfoEntities db = new MemberInfoEntities())
            {
                string searchText = UserInput.Text;   //get text from search box  
                List<member> matchingMembers = GetMembersFromDb(searchText, db);

                DisplayMatches(matchingMembers);
            }
        }

        //get matching persons from db
        //this method returns a member whos first or last name matchs the search term.
        private List<member> GetMembersFromDb(string searchText,MemberInfoEntities db)
        {
           IEnumerable<member> matchingNames = from person in db.members
                                               where person.name.Equals(searchText)
                                               || person.sirname.Equals(searchText)
                                               select person;
            List<member> matchingMembers = matchingNames.ToList();
            return matchingMembers;

        }

        //display persons selected in window
        private void DisplayMatches(List<member> membersList)
        {
            foreach (member m in membersList)
            {
                string fileName;
                if (m.pic.Equals("pic1.png")) fileName = "pic1.PNG";        //assign  male picture
                else fileName = "pic2.PNG";          //assign female picture
    
                //make the Image
                Image mImage = new Image();
                Uri picUri = new Uri(fileName,UriKind.Relative);
                BitmapImage bmpImage = new BitmapImage(picUri);
                mImage.Source = bmpImage;

                //Make the TextBlock
                TextBlock mInfo = new TextBlock();
                mInfo.FontSize = 18;
                mInfo.Text = m.ToString();

                //creat list itme and add Image and TextBlock to it. 
                StackPanel listItem = new StackPanel();
                listItem.Orientation = Orientation.Horizontal;
                listItem.Children.Add(mImage);
                listItem.Children.Add(mInfo);


                MatchingStack.Children.Add(listItem);

             }               
        }   
    }
}
