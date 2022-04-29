using System;
using System.Collections.Generic;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.Models;
using StudentMultiTool.Backend.Services.Matching;

using Xunit;

namespace Tests.Backend.Services.MatchingTests
{
    public class MatchingUnitTests
    {

        // Matching activity profile first with someone who has already crreated activity profile, then with a person who has not
        [Fact]
        public void MatchActivityTest()
        {
            string username = "abrio";
            bool findsMatch;
            findsMatch = Matching.MatchingActivity(username);
            Assert.True(findsMatch);

            string username2 = "mkrisel";
            bool findsMatch2;
            findsMatch2 = Matching.MatchingActivity(username2);
            Assert.False(findsMatch2);
        }

        // Matching tutoring profile first with someone who has already crreated tutoring profile, then with a person who has not
        [Fact]
        public void MatchTutoringTest()
        {
            string username = "abrio";
            bool findsMatchSuccess;
            findsMatchSuccess = Matching.MatchingTutoring(username);
            Assert.True(findsMatchSuccess);

            string username2 = "mkrisel";
            bool findsMatchFail;
            findsMatchFail = Matching.MatchingTutoring(username2);
            Assert.False(findsMatchFail);
        }

        // Creates/Updates tutoring profile first with someone who has already crreated tutoring profile, then with a person who has not
        [Fact]
        public void ActivityProfileTest()
        {
            string username = "abrio";
            List<string> activities = new List<string>();
            activities.Add("Studying");
            activities.Add("Get food (on/off campus)");
            activities.Add("Hang out");
            bool opt = true;
            bool activityProfileUpdate;
            activityProfileUpdate = Activity.ActivityProfile(activities, username, opt);
            Assert.True(activityProfileUpdate);

            string username2 = "dpatel";
            bool activityProfileCreate;
            activityProfileCreate = Activity.ActivityProfile(activities, username2, opt);
            Assert.True(activityProfileCreate);
        }

        // Creates/Updates tutoring profile first with someone who has already crreated tutoring profile, then with a person who has not
        [Fact]
        public void TutoringProfileTest()
        {
            string username = "abrio";
            List<string> courses = new List<string>();
            courses.Add("CECS 328");
            courses.Add("MATH 122");
            bool indivdual = true;
            bool requires = true;
            bool opt = true;
            bool tutoringProfileUpdate;
            tutoringProfileUpdate = Tutoring.TutoringProfile(courses, username, indivdual, requires, opt);
            Assert.True(tutoringProfileUpdate);

            string username2 = "dpatel";
            bool tutoringProfileCreate;
            tutoringProfileCreate = Tutoring.TutoringProfile(courses, username, indivdual, requires, opt);
            Assert.True(tutoringProfileCreate);
        }


        // Tries to add in more than 5 activities to profile
        [Fact]
        public void ActivityProfileFailTest()
        {
            string username = "atoscano";
            List<string> activities = new List<string>();
            activities.Add("Studying");
            activities.Add("Get food (on/off campus)");
            activities.Add("Hang out");
            activities.Add("Exercising");
            activities.Add("Other");
            activities.Add("Go to event");
            bool opt = true;
            bool activityProfileFail;
            activityProfileFail = Activity.ActivityProfile(activities, username, opt);
            Assert.False(activityProfileFail);
        }

        // Incorrect profile information, including more than 6 coursess
        [Fact]
        public void TutoringProfileFailTest()
        {
            string username = "abrio";
            List<string> courses = new List<string>();
            courses.Add("CECS 328");
            courses.Add("MATH 122");
            courses.Add("CECS 323");
            courses.Add("MATH 323");
            courses.Add("CECS 453");
            courses.Add("MATH 380");
            courses.Add("CECS 475");
            courses.Add("MATH 124");
            bool individual = false;
            bool requires = false;
            bool opt = true;
            bool tutoringProfileFail;
            tutoringProfileFail = Tutoring.TutoringProfile(courses, username, individual, requires, opt);
            Assert.False(tutoringProfileFail);
        }

        // User opting out of matching 
        [Fact]
        public void OptOutTest()
        {
            string username = "abrio";
            bool opt = false;
            bool isOptedOut;
            isOptedOut = Matching.UpdateOptStatus(username, opt);
            Assert.True(isOptedOut);
        }

        // User opting in of matching
        [Fact]
        public void OptInTest()
        {
            string username = "abrio";
            bool opt = true;
            bool isOptedIn;
            isOptedIn = Matching.UpdateOptStatus(username, opt);
            Assert.True(isOptedIn);
        }

    }
}
