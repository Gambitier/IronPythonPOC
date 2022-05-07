using System.Collections.Generic;

namespace IronPythonPOC
{
    public class Teacher
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public int CurrentClass { get; set; }
    }

    public class TestPythonActivity
    {
        private readonly Teacher _teacher;

        public TestPythonActivity()
        {
            _teacher = new Teacher()
            {
                Name = "John",
                Surname = "Samuel",
                Age = 56,
                Students = new List<Student>()
                {
                    new Student()
                    {
                        Name = "John",
                        Surname = "Keen",
                        Age = 23,
                        CurrentClass = 12
                    },
                    new Student()
                    {
                        Name = "Akash",
                        Surname = "Jadhav",
                        Age = 20,
                        CurrentClass = 10
                    },
                    new Student()
                    {
                        Name = "Abhishek",
                        Surname = "Patil",
                        Age = 25,
                        CurrentClass = 14
                    },
                }
            };
        }

        public dynamic TestPythonCode()
        {
            string code = @"
class PyClass:
    def __init__(self):
        pass

    def getTotal(self, n):
        return (n + oddNumber + teacher.Students[1].Age)

    def getSecondStudent(self):
        return teacher.Students[1]

    def getModifiedStudentData(self):
        student = teacher.Students[1]
        return {
            'name': student.Name,
            'familyName': student.Name,
            'age': student.Age,
        }
";
            PythonActivity pythonActivity = new PythonActivity();
            pythonActivity.CreatePythonActivity(code);
            pythonActivity.SetVariable("oddNumber", 13);
            pythonActivity.SetVariable("teacher", _teacher);

            var getTotal = pythonActivity.CallFunction("getTotal", 6);
            var getSecondStudent = pythonActivity.CallFunction("getSecondStudent");
            var getModifiedStudentData = pythonActivity.CallFunction("getModifiedStudentData");


            return new
            {
                getTotal,
                getSecondStudent,
                getModifiedStudentData
            };
        }
    }
}
