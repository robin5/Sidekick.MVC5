using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ResultsViewModel
    {
        public ResultsViewModel(int id)
        {
            Name = "Survey-1-1";

            var team1 = new SurveySummaryTeam("Team #1");
            var team2 = new SurveySummaryTeam("Team #2");
            var team3 = new SurveySummaryTeam("Team #3");
            var team4 = new SurveySummaryTeam("Team #4");
            var team5 = new SurveySummaryTeam("Team #5");

            team1.Students.Add(new SurveySummaryStudent { UserName = "rlint", Name = "Richard Lint", id = 36, Complete = 0.667f });
            team1.Students.Add(new SurveySummaryStudent { UserName = "pmcculley", Name = "Patrick McCulley", id = 37, Complete = 0.667f });
            team1.Students.Add(new SurveySummaryStudent { UserName = "ademchenko", Name = "Andrey Demchenko", id = 38, Complete = 0.667f });
            team1.Students.Add(new SurveySummaryStudent { UserName = "robin", Name = "Robin Murray", id = 39, Complete = 0.667f });

            team2.Students.Add(new SurveySummaryStudent { UserName = "dking", Name = "Dave King", id = 40, Complete = 0.667f });
            team2.Students.Add(new SurveySummaryStudent { UserName = "ajaeger", Name = "Amy Jaeger", id = 41, Complete = 0.667f });
            team2.Students.Add(new SurveySummaryStudent { UserName = "belgort", Name = "Bruce Elgort", id = 42, Complete = 0.667f });
            team2.Students.Add(new SurveySummaryStudent { UserName = "drichards", Name = "David Richards", id = 43, Complete = 0.667f });

            team3.Students.Add(new SurveySummaryStudent { UserName = "wwoods", Name = "Wayne Woods", id = 44, Complete = 0.667f });
            team3.Students.Add(new SurveySummaryStudent { UserName = "yshapovalov", Name = "Yevgen Shapovalov", id = 45, Complete = 0.667f });
            team3.Students.Add(new SurveySummaryStudent { UserName = "jruff", Name = "Jacob Ruff", id = 46, Complete = 0.667f });
            team3.Students.Add(new SurveySummaryStudent { UserName = "cmcguire", Name = "Chris McGuire", id = 47, Complete = 0.667f });

            team4.Students.Add(new SurveySummaryStudent { UserName = "mlehr", Name = "Matt Lehr", id = 48, Complete = 0.667f });
            team4.Students.Add(new SurveySummaryStudent { UserName = "bsejouk", Name = "Bilal Sejouk", id = 49, Complete = 0.667f });
            team4.Students.Add(new SurveySummaryStudent { UserName = "jglenn", Name = "John Glenn", id = 50, Complete = 0.667f });
            team4.Students.Add(new SurveySummaryStudent { UserName = "alevine", Name = "Adam Levine", id = 51, Complete = 0.667f });

            team5.Students.Add(new SurveySummaryStudent { UserName = "jlopez", Name = "Jennifer Lopez", id = 52, Complete = 0.667f });
            team5.Students.Add(new SurveySummaryStudent { UserName = "spenn", Name = "Sean Penn", id = 53, Complete = 0.667f });
            team5.Students.Add(new SurveySummaryStudent { UserName = "reaton", Name = "Richard Eaton", id = 54, Complete = 0.667f });
            team5.Students.Add(new SurveySummaryStudent { UserName = "test_student1", Name = "First1 Last1", id = 55, Complete = 0.667f });
            team5.Students.Add(new SurveySummaryStudent { UserName = "abunker", Name = "Archie Bunker", id = 56, Complete = 0.667f });


            _teams = new List<Team>();
            _teams.Add(team1);
            _teams.Add(team2);
            _teams.Add(team3);
            _teams.Add(team4);
            _teams.Add(team5);
        }
        private List<Team> _teams;
        public string Name { get; set; }
        public IEnumerable<Team> Teams { get { return _teams; } }
        public string PieData()
        {
            return "[['A',2],['B',3],['Not Available',2]]";
        }
    }
}