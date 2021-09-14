using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;
using System.Linq;
using TrackerLibrary.DataAccess.TextConnection;

namespace TrackerLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {

        private const string PrizeFile = "prizes.text";
        private const string PersonFile = "person.text";
        private const string TeamFIle = "teams.text";
        private const string TournamentFile = "tournaments.text";
        private const string MatchupFile = "matchup.text";
        private const string MatchupEntryFile = "matchupentry.text";

        // TODO - maKE TEEXT CONNECTIONS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public PrizeModel createPrize(PrizeModel model)
        {
            List<PrizeModel> prizes;
            //Load text file
            //Convert to list of model
            //add model to list
            //writelist back to file
            prizes = PrizeFile.fullFilePath().loadFile().convertToPrizeModel();
            if (prizes.Count != 0)
            {
                int currentid = prizes.OrderByDescending(x => x.id).First().id + 1;
                model.id = currentid;
            }
            else
            {
                model.id = 1;
            }
            prizes.Add(model);
            PrizeFile.fullFilePath().saveToPrizeFile(prizes);
            return model;
        }

        public PersonModel createPerson(PersonModel model)
        {
            List<PersonModel> people;
            //Load text file
            //Convert to list of model
            //add model to list
            //writelist back to file
            people = PersonFile.fullFilePath().loadFile().convertToPersonModel();

            if (people.Count != 0)
            {
                int currentid = people.OrderByDescending(x => x.id).First().id + 1;
                model.id = currentid;
            }
            else
            {
                model.id = 1;
            }
            people.Add(model);
            PersonFile.fullFilePath().saveToPersonFile(people);
            return model;
        }

        public List<PersonModel> getPeopleAll()
        {
            List<PersonModel> people;
            people = PersonFile.fullFilePath().loadFile().convertToPersonModel();
            return people;
        }

        public TeamModel createTeam(TeamModel model)
        {
            List<TeamModel> teams;
            teams = TeamFIle.fullFilePath().loadFile().convertToTeamModel();
            
            if (teams.Count != 0)
            {
                int currentid = teams.OrderByDescending(x => x.id).First().id + 1;
                model.id = currentid;
            }
            else
            {
                model.id = 1;
            }
            teams.Add(model);
            TeamFIle.fullFilePath().saveToTeamFile(teams);
            return model;
        }

        public List<TeamModel> getTeamAll()
        {
            List<TeamModel> teams;
            List<PersonModel> people;
            List<PersonModel> _people;
            teams = TeamFIle.fullFilePath().loadFile().convertToTeamModel();
            people = PersonFile.fullFilePath().loadFile().convertToPersonModel();
            foreach (TeamModel team in teams)
            {
                _people = new List<PersonModel>();
                foreach (PersonModel person in team.TeamMembers)
                {
                    PersonModel p = new PersonModel();
                    p = people.Find(x => x.id == person.id);
                    _people.Add(p);
                }
                team.TeamMembers = _people;
            }
            return teams;
        }

        public TournamentModel createTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments;
            tournaments = TournamentFile.fullFilePath().loadFile().convertToTournamentModel();
            List<TeamModel> team = getTeamAll();

            if (tournaments.Count != 0)
            {
                int currentid = tournaments.OrderByDescending(x => x.id).First().id + 1;
                model.id = currentid;
            }
            else
            {
                model.id = 1;
            }

            List<MatchupModel> matchups = MatchupFile.fullFilePath().loadFile().convertToMatchupModel();
            List<MatchupEntryModel> entries = MatchupEntryFile.fullFilePath().loadFile().convertToMatchupEntryModel();

            foreach (MatchupEntryModel entry in entries)
            {
                entry.TeamCompeting = team.Find(x => x.id == entry.TeamCompetingid);
                entry.ParentMatcup = matchups.Find(x => x.id == entry.ParentMatchupid);
            }

            int matchupid;
            int entryid;
            if (matchups.Count != 0)
            {
                matchupid = matchups.OrderByDescending(x => x.id).First().id + 1;
            }
            else
            {
                matchupid = 1;
            }
            if (entries.Count != 0)
            {
                entryid = entries.OrderByDescending(x => x.id).First().id + 1;
            }
            else
            {
                entryid = 1;
            }



            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    matchup.id = matchupid;
                    matchups.Add(matchup);

                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        entry.id = entryid;
                        entries.Add(entry);
                        entryid++;
                    }
                    matchupid++;
                }
            }

            tournaments.Add(model);
            TournamentFile.fullFilePath().saveToTournamentFile(tournaments);
            MatchupFile.fullFilePath().saveToMatchupFile(matchups);
            MatchupEntryFile.fullFilePath().saveToMatchupEntryFile(entries);
            
            return model;
        }

        private List<MatchupModel> getMatchupsAll()
        {
            List<MatchupModel> output;
            List<MatchupEntryModel> entries = MatchupEntryFile.fullFilePath().loadFile().convertToMatchupEntryModel();
            List<TeamModel> team = getTeamAll();
            List<MatchupEntryModel> _entries;

            output = MatchupFile.fullFilePath().loadFile().convertToMatchupModel();
            foreach (MatchupEntryModel entry in entries)
            {
                entry.TeamCompeting = team.Find(x => x.id == entry.TeamCompetingid);
                entry.ParentMatcup = output.Find(x => x.id == entry.ParentMatchupid);
            }

            foreach (MatchupModel matchup in output)
            {
                _entries = new List<MatchupEntryModel>();
                matchup.Winner = team.Find(x => x.id == matchup.Winnerid);

                foreach (MatchupEntryModel entry in matchup.Entries)
                {
                    MatchupEntryModel m = new MatchupEntryModel();
                    m = entries.Find(x => x.id == entry.id);
                    _entries.Add(m);
                }
                matchup.Entries = _entries;
            }
            return output;
        }

        public List<TournamentModel> getTournamentAll()
        {
            List<TournamentModel> output = TournamentFile.fullFilePath().loadFile().convertToTournamentModel();
            List<TeamModel> teams = getTeamAll();
            List<PrizeModel> prizes = PrizeFile.fullFilePath().loadFile().convertToPrizeModel();
            List<MatchupModel> matchups = getMatchupsAll();
            List<TeamModel> _teams;
            List<PrizeModel> _prizes;
            List<MatchupModel> _matchups;
            List<List<MatchupModel>> rounds;

            foreach (TournamentModel tournament in output)
            {
                _teams = new List<TeamModel>();
                _prizes = new List<PrizeModel>();
                rounds = new List<List<MatchupModel>>();
                foreach (TeamModel team in tournament.EnteredTeams)
                {
                    TeamModel t = new TeamModel();
                    t = teams.Find(x => x.id == team.id);
                    _teams.Add(t);
                }
                tournament.EnteredTeams = _teams;

                foreach (PrizeModel prize in tournament.Prizes)
                {
                    PrizeModel p = new PrizeModel();
                    p = prizes.Find(x => x.id == prize.id);
                    _prizes.Add(p);
                }
                tournament.Prizes = _prizes;

                foreach (List<MatchupModel> round in tournament.Rounds)
                {
                    _matchups = new List<MatchupModel>();
                    foreach (MatchupModel matchup in round)
                    { 
                        MatchupModel m = new MatchupModel();
                        m = matchups.Find(x => x.id == matchup.id);
                        _matchups.Add(m);
                    }
                    rounds.Add(_matchups);
                }
                tournament.Rounds = rounds;
            }
            return output;
        }

        public void updateMatchupEntry(MatchupEntryModel model)
        {
            List<TeamModel> team = getTeamAll();
            List<MatchupModel> matchups = MatchupFile.fullFilePath().loadFile().convertToMatchupModel();
            List<MatchupEntryModel> entries = MatchupEntryFile.fullFilePath().loadFile().convertToMatchupEntryModel();
            foreach (MatchupEntryModel entry in entries)
            {
                entry.TeamCompeting = team.Find(x => x.id == entry.TeamCompetingid);
                entry.ParentMatcup = matchups.Find(x => x.id == entry.ParentMatchupid);
            }
            int index = entries.FindIndex(x => x.id == model.id);
            entries[index].TeamCompetingid = model.TeamCompetingid;
            entries[index].TeamCompeting = model.TeamCompeting;
            MatchupEntryFile.fullFilePath().saveToMatchupEntryFile(entries);
        }

        public void updateMatchup(MatchupModel model)
        {
            List<MatchupModel> matchups = getMatchupsAll();
            int index = matchups.FindIndex(x => x.id == model.id);
            matchups[index].Winnerid = model.Winnerid;
            matchups[index].Winner = model.Winner;
            MatchupFile.fullFilePath().saveToMatchupFile(matchups);
        }

        public void updateMatchupEntryScore(MatchupEntryModel model)
        {
            List<TeamModel> team = getTeamAll();
            List<MatchupModel> matchups = MatchupFile.fullFilePath().loadFile().convertToMatchupModel();
            List<MatchupEntryModel> entries = MatchupEntryFile.fullFilePath().loadFile().convertToMatchupEntryModel();
            foreach (MatchupEntryModel entry in entries)
            {
                entry.TeamCompeting = team.Find(x => x.id == entry.TeamCompetingid);
                entry.ParentMatcup = matchups.Find(x => x.id == entry.ParentMatchupid);
            }
            int index = entries.FindIndex(x => x.id == model.id);
            entries[index].Score = model.Score;
            MatchupEntryFile.fullFilePath().saveToMatchupEntryFile(entries);
        }

        public void updateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentFile.fullFilePath().loadFile().convertToTournamentModel();
            int index = tournaments.FindIndex(x => x.id == model.id);
            tournaments[index].Active = 0;
            TournamentFile.fullFilePath().saveToTournamentFile(tournaments);
        }
    }
}
