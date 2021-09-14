using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Linq;
using TrackerLibrary.Models;


namespace TrackerLibrary.DataAccess.TextConnection
{
    public static class TextConnectorHelper
    {

        public static void saveToMatchupEntryFile(this string file, List<MatchupEntryModel> entries)
        {
            List<string> lines = new List<string>();
            foreach (MatchupEntryModel entry in entries)
            {
                StringBuilder s = new StringBuilder($"{entry.id},{entry.Score},");
                if (entry.TeamCompeting != null)
                {
                    s.Append($"{entry.TeamCompeting.id}");
                }
                else
                {
                    s.Append('-');
                }
                s.Append(',');
                if (entry.ParentMatcup != null)
                {
                    s.Append($"{entry.ParentMatcup.id}");
                }
                else
                {
                    s.Append('-');
                }
                lines.Add(s.ToString());

            }
            File.WriteAllLines(file, lines);
        }

        public static void saveToMatchupFile(this string file, List<MatchupModel> matchups)
        {
            List<string> lines = new List<string>();
            foreach (MatchupModel matchup in matchups)
            {
                StringBuilder s = new StringBuilder($"{matchup.id},{matchup.MatchupRound},");
                if (matchup.Winner != null)
                {
                    s.Append($"{matchup.Winner.id},");
                }
                else
                {
                    s.Append("-,");
                }
                for (int i = 0; i < matchup.Entries.Count; i++)
                {
                    s.Append($"{matchup.Entries[i].id}");
                    if (i < matchup.Entries.Count - 1)
                    {
                        s.Append('|');
                    }
                }
                lines.Add(s.ToString());
            }
            File.WriteAllLines(file, lines);
        }

        public static void saveToTournamentFile(this string file,List<TournamentModel> tournaments)
        {
            List<string> lines = new List<string>();
            foreach (TournamentModel tournament in tournaments)
            {
                StringBuilder s = new StringBuilder($"{tournament.id},{tournament.TournamentName},{tournament.EntryFee},{tournament.Active},");
                for (int i = 0; i < tournament.EnteredTeams.Count; i++)
                {
                    s.Append(tournament.EnteredTeams[i].id);
                    if (i < tournament.EnteredTeams.Count - 1)
                    {
                        s.Append('|');
                    }
                }
                s.Append(",");

                if (tournament.Prizes.Count != 0)
                {
                    for (int i = 0; i < tournament.Prizes.Count; i++)
                    {
                        s.Append(tournament.Prizes[i].id);
                        if (i < tournament.Prizes.Count - 1)
                        {
                            s.Append('|');
                        }
                    } 
                }
                else
                {
                    s.Append("-");  
                }
                s.Append(",");

                for (int i = 0; i < tournament.Rounds.Count; i++)
                {
                    for (int j = 0; j < tournament.Rounds[i].Count; j++)
                    {
                        s.Append(tournament.Rounds[i][j].id);
                        if (j < tournament.Rounds[i].Count - 1)
                        {
                            s.Append("/");
                        }
                    }
                    if (i < tournament.Rounds.Count - 1)
                    {
                        s.Append("|");
                    }
                }
                lines.Add(s.ToString());
            }
            File.WriteAllLines(file, lines);
        }

        public static void saveToTeamFile(this string file, List<TeamModel> teams)
        {
            List<string> lines = new List<string>();
            foreach (TeamModel team in teams)
            {
                StringBuilder t = new StringBuilder($"{team.id},{team.TeamName},");
                for (int i = 0; i < team.TeamMembers.Count; i++)
                {
                    t.Append(team.TeamMembers[i].id);
                    if (i < team.TeamMembers.Count - 1)
                    {
                        t.Append("|");
                    }
                }
                lines.Add(t.ToString());
            }
            File.WriteAllLines(file, lines);
        }

        public static void saveToPersonFile(this string file, List<PersonModel> people)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel person in people)
            {
                lines.Add($"{person.id},{person.FirstName},{person.LastName},{person.Email},{person.CellPhoneNumber}");
            }
            File.WriteAllLines(file, lines);

        }

        public static void saveToPrizeFile(this string file, List<PrizeModel> prizes)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel prize in prizes)
            {
                switch (prize.Type)
                {
                    case PrizeType.Amount:
                        lines.Add($"{prize.id},{prize.PlaceNumber},{prize.PlaceName},{prize.PrizeAmount},{0}");
                        break;
                    case PrizeType.Percentage:
                        lines.Add($"{prize.id},{prize.PlaceNumber},{prize.PlaceName},{prize.PrizePercentage},{1}");
                        break;
                }
            }

            File.WriteAllLines(file, lines);
            

        }

        public static List<MatchupEntryModel> convertToMatchupEntryModel(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            foreach (string line in lines)
            {
                MatchupEntryModel m = new MatchupEntryModel();
                string[] cols = line.Split(',');

                m.id = Int32.Parse(cols[0]);
                m.Score = Double.Parse(cols[1]);
                if (cols[2] != "-")
                {
                    m.TeamCompetingid = Int32.Parse(cols[2]);
                }
                if (cols[3] != "-")
                {
                    m.ParentMatchupid = Int32.Parse(cols[3]);
                } 

                output.Add(m);
            }
            return output;
        }

        public static List<MatchupModel> convertToMatchupModel(this List<string> lines)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            foreach (string line in lines)
            {
                MatchupModel m = new MatchupModel();
                string[] cols = line.Split(',');

                m.id = Int32.Parse(cols[0]);
                m.MatchupRound = Int32.Parse(cols[1]);
                if (cols[2] != "-")
                {
                    m.Winnerid = Int32.Parse(cols[2]); 
                }
                string entries = cols[3];

                string[] _entries = entries.Split('|');
                for (int i = 0; i < _entries.Length; i++)
                {
                    m.Entries.Add(new MatchupEntryModel());
                    m.Entries[i].id = Int32.Parse(_entries[i]);
                }
                output.Add(m);
            }
            return output;
        }

        public static List<TournamentModel> convertToTournamentModel(this List<string> lines)
        {
            List<TournamentModel> output = new List<TournamentModel>();
            //List<MatchupModel> matchups = new List<MatchupModel>();
            foreach (string line in lines)
            {
                TournamentModel t = new TournamentModel();
                string[] cols = line.Split(',');
                t.id = Int32.Parse(cols[0]);
                t.TournamentName = cols[1];
                t.EntryFee = decimal.Parse(cols[2]);
                t.Active = Int32.Parse(cols[3]);

                string teams = cols[4];
                string prizes = cols[5];
                string rounds = cols[6];

                string[] _teams = teams.Split('|');
                string[] _prizes = prizes.Split('|');
                string[] _rounds = rounds.Split('|');

                for (int i = 0; i < _teams.Length; i++)
                {
                    t.EnteredTeams.Add(new TeamModel());
                    t.EnteredTeams[i].id = Int32.Parse(_teams[i]);
                }

                if (_prizes[0] != "-")
                {
                    for (int i = 0; i < _prizes.Length; i++)
                    {
                        t.Prizes.Add(new PrizeModel());
                        t.Prizes[i].id = Int32.Parse(_prizes[i]);
                    } 
                }

                for (int i = 0; i < _rounds.Length; i++)
                {
                    t.Rounds.Add(new List<MatchupModel>());
                    string[] matchup = _rounds[i].Split('/');
                    for (int j = 0; j < matchup.Length; j++)
                    {
                        t.Rounds[i].Add(new MatchupModel());
                        t.Rounds[i][j].id = Int32.Parse(matchup[j]);
                    }

                }
                output.Add(t);
            }
            return output;
        }

        public static List<TeamModel> convertToTeamModel(this List<string> lines)
        {
            List<TeamModel> output = new List<TeamModel>();
            foreach (string line in lines)
            {
                TeamModel t = new TeamModel();
                string[] cols = line.Split(',');

                t.id = Int32.Parse(cols[0]);
                t.TeamName = cols[1];
                string teamMembers = cols[2];

                string[] members = teamMembers.Split('|');
                for (int i = 0; i < members.Length; i++)
                {
                    t.TeamMembers.Add(new PersonModel());
                    t.TeamMembers[i].id = Int32.Parse(members[i]);
                }
                output.Add(t);
            }
            return output;
        }

        public static List<PersonModel> convertToPersonModel(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in lines)
            {
                PersonModel p = new PersonModel();
                string[] cols = line.Split(',');

                p.id = Int32.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.Email = cols[3];
                p.CellPhoneNumber = cols[4];
                output.Add(p);
            }
            return output;
        }

        public static List<PrizeModel> convertToPrizeModel(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();
            foreach (string line in lines)
            {
                PrizeModel p = new PrizeModel();
                string[] cols = line.Split(',');

                p.id = Int32.Parse(cols[0]);
                p.PlaceNumber = Int32.Parse(cols[1]);
                p.PlaceName = cols[2];
                int type = Int32.Parse(cols[4]);
                if (type == 0)
                {
                    p.PrizeAmount = decimal.Parse(cols[3]);
                    p.Type = PrizeType.Amount;
                }
                else if (type == 1)
                {
                    p.PrizePercentage = double.Parse(cols[3]);
                    p.Type = PrizeType.Percentage;
                }
                output.Add(p);
            }
            return output;
        }

        public static List<string> loadFile(this string file)
        {
            List<string> output;
            if (File.Exists(file))
            {
                output = File.ReadAllLines(file).ToList();
                return output;
                
            }
            else
            {
                return new List<string>();
            }
        }

        public static string fullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

    }
}
