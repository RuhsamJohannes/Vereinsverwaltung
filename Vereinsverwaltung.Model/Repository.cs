using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Vereinsverwaltung.Model
{
    /// <summary>
    /// Repository als Singleton, damit die Daten aus dem CSV-File nur einmal gelesen werden!
    /// </summary>
    public class Repository
    {
        private const string _fileName = "Members.csv";

        private static Repository _instance;

        private List<Member> _members;

        private Repository()
        {
            _members = new List<Member>();
            LoadMembersFromCsv();
        }

        public static Repository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Repository();
            }

            return _instance;
        }

        /// <summary>
        /// Lädt die Daten vom csv-File in die Collection
        /// </summary>
        private void LoadMembersFromCsv()
        {
            string[][] memberCsv = _fileName.ReadStringMatrixFromCsv(false);
            _members = memberCsv.GroupBy(line => new { FirstName = line[0], LastName = line[1], Birthday = line[2]}).Select(x =>
                new Member
                {
                    FirstName = x.Key.FirstName,
                    LastName = x.Key.LastName,
                    Birthday = x.Key.Birthday
                }).ToList();
        }

        public void AddMember(Member member)
        {
            _members.Add(member);
        }

        public void RemoveMember(Member member)
        {
            _members.Remove(member);
        }

        public List<Member> GetAllMembers()
        {
            return _members.OrderBy(x => x.FullName).ToList(); //Erstellt neue Liste!
        }
    }
}
