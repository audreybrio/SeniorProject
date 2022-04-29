using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.DAL;

namespace StudentMultiTool.Backend.Services.Matching
{
    public class Activity
    {
        public static bool ActivityProfile(List<string> activities, string username, bool opt)
        {

            int listSize = activities.Count;
            bool isSuccess = false;
            if(listSize > 5)
            {
                return false;
            }

            int remainder = 5 - listSize;
            for (int i = 0; i < remainder; i++)
            {
                activities.Add("null");
            }

            string activity1 = activities[0];
            string activity2 = activities[1];
            string activity3 = activities[2];
            string activity4 = activities[3];
            string activity5 = activities[4];

            int userExists = ProfileExists(username);

            if (userExists != 0)
            {
                isSuccess = ActivityDAL.ActivityProfileUpdate(activity1, activity2, activity3, activity4, activity5, username);
            }

            else
            {
                isSuccess = ActivityDAL.ActivityProfileInsert(activity1, activity2, activity3, activity4, activity5, username, opt);
            }



            return isSuccess;
        }

        public static int ProfileExists(string username)
        {
            int countProfile = ActivityDAL.ProfileExists(username);
            return countProfile;
        }
    }

}
