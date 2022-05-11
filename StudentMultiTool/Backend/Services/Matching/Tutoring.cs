using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.DAL;
namespace StudentMultiTool.Backend.Services.Matching
{
    public class Tutoring
    {
        // Gets information from what user has entered to send and store in backend 
        TutoringDAL tutor = new TutoringDAL();
        public bool TutoringProfile(List<string> courses, string username, bool individual, bool requires, bool opt)
        {
            bool isSuccess = false;
            int listSize = courses.Count;
            // Users can only enter upto 6 courses 
            if (listSize > 6){ return false; }
            int remainder = 6 - listSize;
            for (int i = 0; i < remainder; i++)
            {
                courses.Add("null");
            }

            string course1 = courses[0];
            string course2 = courses[1];
            string course3 = courses[2];
            string course4 = courses[3];
            string course5 = courses[4];
            string course6 = courses[5];

            int userExists = ProfileExists(username);

            // Checks if profile exists already, if yes updates, if no inserts
            if (userExists != 0)
            { 
                 isSuccess = tutor.TutoringProfileUpdate(course1, course2, course3, course4, course5, course6, username, individual, requires);
            }

            else
            {
                isSuccess = tutor.TutoringProfileInsert(course1, course2, course3, course4, course5, course6, username, individual, requires, opt);
            }

            return isSuccess ;
        }

        // Checks if profle exists 
        public int ProfileExists(string username)
        {
            int countProfile = tutor.ProfileExists(username);
            return countProfile;
        }


    }
}
