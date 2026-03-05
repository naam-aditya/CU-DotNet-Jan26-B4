using System.Text;

namespace CollegeManagementSystem;

public class Program
{
    public static void Main()
    {
        CollageManagement cm = new();

        while (true)
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) 
                break;
            
            var parts = input.Split(' ');

            if (parts[0] == "ADD")
                cm.AddStudent(parts[1], parts[2], int.Parse(parts[3]));
            else if (parts[0] == "REMOVE")
                cm.RemoveStudent(parts[1]);
            else if (parts[0] == "TOP")
            {
                string result = cm.TopStudent(parts[1]);
                if (!string.IsNullOrEmpty(result))
                    Console.WriteLine(result);
            }
            else if (parts[0] == "RESULT")
            {
                string result = cm.Result();
                if (!string.IsNullOrEmpty(result))
                    Console.WriteLine(result);
            }
        }
    }

    class CollageManagement
    {
        Dictionary<string, Dictionary<string, int>> _studentRecords = [];
        Dictionary<string, LinkedList<KeyValuePair<string, int>>> _studentSubjectsOrder = [];

        Dictionary<string, Dictionary<string, int>> _subjectsRecords = [];
        Dictionary<string, LinkedList<KeyValuePair<string, int>>> _subjectsStudentsOrder = [];

        public int AddStudent(string studentId, string subject, int marks)
        {
            if (!_studentRecords.ContainsKey(studentId))
            {
                _studentRecords[studentId] = [];
                _studentSubjectsOrder[studentId] = [];
            }
            
            if (!_subjectsRecords.ContainsKey(subject))
            {
                _subjectsRecords[subject] = [];
                _subjectsStudentsOrder[subject] = [];
            }

            if (_studentRecords[studentId].ContainsKey(subject))
            {
                int oldMarks = _studentRecords[studentId][subject];

                if (marks > oldMarks)
                {
                    _studentRecords[studentId][subject] = marks;
                    _subjectsRecords[subject][studentId] = marks;

                    var node = _studentSubjectsOrder[studentId].First;
                    while (node != null)
                    {
                        if (node.Value.Key == subject)
                        {
                            node.Value = new KeyValuePair<string, int>(subject, marks);
                            break;
                        }
                        node = node.Next;
                    }

                    node = _subjectsStudentsOrder[subject].First;
                    while (node != null)
                    {
                        if (node.Value.Key == studentId)
                        {
                            node.Value = new KeyValuePair<string, int>(studentId, marks);
                            break;
                        }

                        node = node.Next;
                    }
                }
            }
            else
            {
                _studentRecords[studentId][subject] = marks;
                _subjectsRecords[subject][studentId] = marks;

                _studentSubjectsOrder[studentId]
                    .AddLast(new KeyValuePair<string, int>(subject, marks));
                _subjectsStudentsOrder[subject]
                    .AddLast(new KeyValuePair<string, int>(studentId, marks));
            }

            return 1;
        }

        public int RemoveStudent(string studentId)
        {
            if (!_studentRecords.ContainsKey(studentId))
                return 0;
            
            foreach (var subject in _studentRecords[studentId].Keys)
            {
                _subjectsRecords[subject].Remove(studentId);

                var node = _subjectsStudentsOrder[subject].First;
                while (node != null)
                {
                    if (node.Value.Key == studentId)
                    {
                        _subjectsStudentsOrder[subject].Remove(node);
                        break;
                    }
                    node = node.Next;
                }
            }

            _studentRecords.Remove(studentId);
            _studentSubjectsOrder.Remove(studentId);

            return 1;
        }

        public string TopStudent(string subject)
        {
            if (!_subjectsStudentsOrder.ContainsKey(subject) || _subjectsStudentsOrder[subject].Count == 0)
                return string.Empty;
            
            int maxMarks = _subjectsStudentsOrder[subject].Max(x => x.Value);
            StringBuilder result = new();

            foreach (var pair in _subjectsStudentsOrder[subject])
                if (pair.Value == maxMarks)
                    result.AppendLine($"{pair.Key} {pair.Value}");
            
            return result.ToString().TrimEnd();
        }

        public string Result()
        {
            StringBuilder result = new();
            foreach (var student in _studentRecords)
            {
                double avg = student.Value.Values.Average();
                result.AppendLine($"{student.Key} {avg.ToString("F2")}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
