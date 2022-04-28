using System;
using System.Collections.Generic;
using StudentMultiTool.Backend.Controllers;
using StudentMultiTool.Backend.Models;
using StudentMultiTool.Backend.Services;
using Xunit;

namespace Tests.Backend.Services.Matching
{
    public class MatchingUnitTests
    {

        // Matching activity profile first with someone who has already crreated activity profile, then with a person who has not
        [Fact]
        public void MatchActivityTest()
        {
            string username = "abrio";
            bool findsMatch;
            //findsMatch = MatchingController.MatchingActivity(username);
            //Assert.True(findsMatch);

            string username2 = "mkrisel";
            bool findsMatch2;
            //findsMatch2 = MatchingController.MatchingActivity(username2);
            //Assert.False(findsMatch2);
        }

        // Matching tutoring profile first with someone who has already crreated tutoring profile, then with a person who has not
        [Fact]
        public void MatchTutoringTest()
        {
            string username = "abrio";
            bool findsMatchSuccess;
            //findsMatchSuccess = MatchingController.MatchingTutoring(username);
            //Assert.True(findsMatchSuccess);

            string username2 = "mkrisel";
            bool findsMatchFail;
            //findsMatchFail = MatchingController.MatchingTutoring(username2);
            //Assert.False(findsMatchFail);
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
            //activityProfileUpdate = ActivityProfileController.ActivityProfile(activities, username, opt);
            //Assert.True(activityProfileUpdate);

            string username2 = "stang";
            bool activityProfileCreate;
            //activityProfileCreate = ActivityProfileController.ActivityProfile(activities, username2, opt);
            //Assert.True(activityProfileCreate);
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
            //tutoringProfileUpdate = TutoringProfileController.TutoringProfile(courses, username, indivdual, requires, opt);
            //Assert.True(tutoringProfileUpdate);

            string username2 = "stang";
            bool tutoringProfileCreate;
            //tutoringProfileCreate = TutoringProfileController.TutoringProfile(courses, username, indivdual, requires, opt);
            //Assert.True(tutoringProfileCreate);
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
            //activityProfileFail = ActivityProfileController.ActivityProfile(activities, username, opt);
            //Assert.False(activityProfileFail);
        }

        // Incorrect profile information, including more than 6 course, and not selecting individual or requires preference
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
            bool? indivdual = null;
            bool? requires = null;
            bool opt = true;
            bool tutoringProfileFail;
            //tutoringProfileFail = TutoringProfileController.TutoringProfile(courses, username, indivdual, requires, opt);
            //Assert.False(tutoringProfileFail);
        }

        // User opting out of matching 
        [Fact]
        public void OptOutTest()
        {
            string username = "abrio";
            bool opt = false;
            bool isOptedOut;
            //isOptedOut = MatchingController.UpdateOptIn(username, opt);
            //Assert.True(isOptedOut);
        }

        // User opting in of matching
        [Fact]
        public void OptInTest()
        {
            string username = "abrio";
            bool opt = true;
            bool isOptedIn;
            //isOptedIn = MatchingController.UpdateOptIn(username, opt);
            //Assert.True(isOptedIn);
        }

    }
}
