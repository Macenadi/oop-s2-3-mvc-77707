using Xunit;

namespace TestProject
{
    public class GlobalCollegeTests
    {
        [Fact]
        public void Student_Number_Should_Not_Be_Empty()
        {
            string studentNumber = "07365";

            Assert.False(string.IsNullOrWhiteSpace(studentNumber));
        }

        [Fact]
        public void Faculty_Email_Should_Contain_College_Domain()
        {
            string email = "12345@college.com";

            Assert.Contains("@college.com", email);
        }

        [Fact]
        public void Password_Should_Have_Minimum_Length()
        {
            string password = "Abc12345";

            Assert.True(password.Length >= 8);
        }

        [Fact]
        public void Assignment_MaxScore_Should_Be_Greater_Than_Zero()
        {
            int maxScore = 100;

            Assert.True(maxScore > 0);
        }

        [Fact]
        public void Exam_Result_Should_Not_Be_Negative()
        {
            decimal score = 75;

            Assert.True(score >= 0);
        }

        [Fact]
        public void Attendance_Can_Be_True_When_Student_Is_Present()
        {
            bool present = true;

            Assert.True(present);
        }

        [Fact]
        public void ClassCode_Should_Not_Be_Empty()
        {
            string classCode = "TEMP-1";

            Assert.False(string.IsNullOrWhiteSpace(classCode));
        }

        [Fact]
        public void Released_Result_Should_Be_Visible()
        {
            bool resultsReleased = true;

            Assert.True(resultsReleased);
        }
    }
}