/// Author: Daniel Anderson  Jan.23,2015
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{

    ///<summary> 
    ///This class is used to imprt the data from a file to the database
    ///it imports from a comma delimited file.  Lines should end in a comma before the new line.
    /////and the list of interestes has each item separated by a ";".  Also the "pic1.png" is for males and "pic2.png" for females.
    ///</summary> 
    class DbImporter
    {
        #region populate database from "data.csv"
        //This method takes a string which is the path for the file. 
        public void FillDB(string fileName)
        {
            using (StreamReader fileReader = new StreamReader(fileName))
            {
                while (!fileReader.EndOfStream)
                {
                    try
                    {
                        string tempMember = fileReader.ReadLine();
                        member newMember = MakeMember(ParseMemberInfo(tempMember));
                        AddToDb(newMember);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("exception: " + exc.Message);
                        Console.WriteLine(exc.StackTrace);
                    }

                }
            }
        }

        //parses the string by a comma ","
        //returns a string list of the member information
        private List<string> ParseMemberInfo(string member)
        {
            List<string> parsedMember = new List<string>();
            StringBuilder strBldr = new StringBuilder();
            foreach (char character in member)
            {
                if (character == ',')
                {
                    string tempStr = strBldr.ToString();
                    parsedMember.Add(tempStr);
                    strBldr.Clear();
                }
                else
                {
                    strBldr.Append(character);
                }
            }
            return parsedMember;
        }


        //make and return a new member for the db
        private member MakeMember(List<string> memberInfo)
        {
            member newMember = new member();
            StringBuilder interests = new StringBuilder();

            newMember.name = memberInfo[0];
            newMember.sirname = memberInfo[1];
            newMember.address = memberInfo[2];
            newMember.age = int.Parse(memberInfo[3]);

            foreach (char c in memberInfo[4])
            {
                if(c==';') interests.Append(", ");
                else interests.Append(c);
            }
            newMember.intrets = interests.ToString();
            newMember.pic = memberInfo[5];

            return newMember;
        }


        //adds a member with their info to database.
        private void AddToDb(member newMember)
        {
            using (MemberInfoEntities db = new MemberInfoEntities())
            {
                db.members.Add(newMember);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
