using Microsoft.AspNetCore.Mvc;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.DAL;

namespace StudentMultiTool.Backend.Services.Matching
{
    public class Activity
    {

        ActivityDAL activity = new ActivityDAL();
        // Get list of activites that user has selcted, and sending them to be added into their saved profiles
        public bool ActivityProfile(List<string> activities, string username, bool opt)
        {

            int listSize = activities.Count;
            bool isSuccess = false;
            // User can only enter in up to 5 activities
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


            // Sees if a profile has already been created, if it has it updates, otherwise it inserts 
            int userExists = ProfileExists(username);

            if (userExists != 0)
            {
                isSuccess = activity.ActivityProfileUpdate(activity1, activity2, activity3, activity4, activity5, username);
            }

            else
            {
                isSuccess = activity.ActivityProfileInsert(activity1, activity2, activity3, activity4, activity5, username, opt);
            }



            return isSuccess;
        }

        // Checks if profile already exists 
        public int ProfileExists(string username)
        {
            int countProfile = activity.ProfileExists(username);
            return countProfile;
        }
    }

}
