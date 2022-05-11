using StudentMultiTool.Backend.Services.Matching;
using StudentMultiTool.Backend.DAL;
using StudentMultiTool.Backend.Models.Matching;

namespace StudentMultiTool.Backend.Services.Matching
{
    public class Matching
    {
        Tutoring tutor = new Tutoring();
        Activity activity = new Activity();
        MatchingDAL match = new MatchingDAL();

        // Matches based off of activity profile 
        public List<Match> MatchingActivity(string username)
        {
            // Sees if profile exists, if it does, continues to match
            List<Match> matches = new List<Match>();
            int profileCount = activity.ProfileExists(username);
            if (profileCount == 0)
            {
                return matches;
            }
            List<string> activities = new List<string>();
            activities = GetActivityProfile(username);
            matches = match.MatchingActivity(activities, username);
            return matches;
        }

        // Matching based off of Tutoring profile
        public List<Match> MatchingTutoring(string username)
        {
            // Sees if profile exists, if it does contrinues to match 
            List<Match> matches = new List<Match>();
            int profileCount = tutor.ProfileExists(username);
            if (profileCount == 0)
            {
                return matches;
            }
            List<string> courses = new List<string>();
            bool individual, requires;
            (courses, individual, requires) = GetTutoringProfile(username);
            matches = match.MatchingTutoring(courses, username, individual, requires);
            return matches;
        }

        // Display Matches to frontend 
        public List<Match> DisplayMatches(string username)
        {
            List<Match> matches = new List<Match>();
            matches = match.DisplayMatches(username);
            return matches;
        }

        // Get activity profile from what user has entered 
        public List<string> GetActivityProfile(string username)
        {
            List<string> activities = new List<string>();
            string activity1, activity2, activity3, activity4, activity5;
            (activity1, activity2, activity3, activity4, activity5) = match.GetActivityProfile(username);

            activities.Add(activity1);
            if (activity2 != "null")
            {
                activities.Add(activity2);
            }
            if (activity3 != "null")
            {
                activities.Add(activity3);
            }
            if (activity4 != "null")
            {
                activities.Add(activity4);
            }
            if (activity5 != "null")
            {
                activities.Add(activity5);
            }
            return activities;
        }

        // Get tutoring profile from what user has entered 
        public (List<string>, bool, bool) GetTutoringProfile(string username)
        {

            List<string> courses = new List<string>();
            string course1, course2, course3, course4 , course5, course6;
            bool individual, requires;
            (course1, course2, course3, course4, course5, course6, individual, requires) = match.GetTutoringProfile(username);

            courses.Add(course1);
            if (course2 != "null")
            {
                courses.Add(course2);
            }
            if (course3 != "null")
            {
                courses.Add(course3);
            }
            if (course4 != "null")
            {
                courses.Add(course4);
            }
            if (course5 != "null")
            {
                courses.Add(course5);
            }
            if (course6 != "null")
            {
                courses.Add(course6);
            }

            return (courses, individual, requires);

        }

        // Insert Match into database
        public bool InsertMatch(string username, int matchId, string reason, string overlap)
        {
            bool matchInserted = match.InsertMatch(username, matchId, reason, overlap);
            return matchInserted;
        }
        // Checks if match already exists 
        public int MatchExists(string username, int matchId, string reason)
        {
            int matchCount = match.MatchExists(username, matchId, reason);
            return matchCount;
        }

        // Checks users opted in/out status
        public bool CheckOptedIn(string username)
        {
            bool checkedOptStatus = match.CheckOptedIn(username);
            return checkedOptStatus;
        }


        // Updates a user's opted/in status
        public bool UpdateOptStatus(string username, bool opt)
        {

            int countActivityProfile = activity.ProfileExists(username);
            int countTutoringProfile = tutor.ProfileExists(username);

            if (countActivityProfile == 0 && countTutoringProfile == 0)
            {
                return false;
            }

            bool updateOpt = match.UpdateOptStatus(username, opt);
            return updateOpt;
        }

    }
}
