using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        private List<int> Rounds = new List<int>();
        private List<MatchupModel> currentRoundMatchups;
        private List<MatchupModel> displayedMatchups;
        private MatchupModel currMatchup;
        private int currRound;
        private int maxRound;
        IReturnToDashboard dashboard;
        public TournamentViewerForm(TournamentModel model,IReturnToDashboard dash)
        {
            InitializeComponent();
            dashboard = dash;
            tournament = model;
            tournament.OnTournamentComplete += Tournament_OnTournamentComplete;
            setUpForm();

        }

        private void Tournament_OnTournamentComplete(object sender, DateTime e)
        {
            //calculate prizes if any
            //declare winner
            TeamModel winner = tournament.Rounds.Last().First().Winner;
            TeamModel runnerUp = tournament.Rounds.Last().First().Entries.First(x => x.TeamCompeting != winner).TeamCompeting;

            winnerLabel.Visible = true;
            winnerNameLabel.Visible = true;
            runnerUpLabel.Visible = true;
            runnerUpNameLabel.Visible = true;

            winnerNameLabel.Text = winner.TeamName;
            runnerUpNameLabel.Text = runnerUp.TeamName;
            //Update database
            GlobalConfig.Connection.updateTournament(tournament);


        }

        private void setUpForm()
        {
            //Set tournament name
            tournamentNameLabel.Text = tournament.TournamentName;
            //Set up rounds
            maxRound = tournament.Rounds.Count;
            for (int i = 1; i <= tournament.Rounds.Count; i++)
            {
                Rounds.Add(i);
            }

            roundComboBox.DataSource = Rounds;
            currRound = (int)roundComboBox.SelectedItem;
            tournament.sortOutByes();
            refreshMatchupListBox();
            currMatchup = (MatchupModel)matchupListBox.SelectedItem;

            if (tournament.Active == 0)
            {
                winnerLabel.Visible = true;
                winnerNameLabel.Visible = true;
                runnerUpLabel.Visible = true;
                runnerUpNameLabel.Visible = true;

                TeamModel winner = tournament.Rounds.Last().First().Winner;
                TeamModel runnerUp = tournament.Rounds.Last().First().Entries.First(x => x.TeamCompeting != winner).TeamCompeting;

                winnerNameLabel.Text = winner.TeamName;
                runnerUpNameLabel.Text = runnerUp.TeamName;
            }

        }

        private void refreshScoreDisplay()
        {
            if (currMatchup != null)
            {
                if (currMatchup.Entries.Count < 2)
                {
                    teamOneScoreTextBox.Text = "0";
                    teamTwoScoreTextBox.Text = "0";

                    scoreButton.Enabled = false;
                    teamOneScoreTextBox.Enabled = false;
                    teamTwoScoreTextBox.Enabled = false;

                    teamOneLabel.Text = currMatchup.Entries[0].TeamCompeting.TeamName;
                    TeamTwoLabel.Text = "bye";
                }
                else
                {
                    if (currMatchup.Entries[0].TeamCompeting != null && currMatchup.Entries[1].TeamCompeting != null)
                    {
                        teamOneLabel.Text = currMatchup.Entries[0].TeamCompeting.TeamName;
                        TeamTwoLabel.Text = currMatchup.Entries[1].TeamCompeting.TeamName;

                        if (currMatchup.Winner != null)
                        {
                            teamOneScoreTextBox.Text = $"{currMatchup.Entries[0].Score}";
                            teamTwoScoreTextBox.Text = $"{currMatchup.Entries[1].Score}";

                            scoreButton.Enabled = false;
                            teamOneScoreTextBox.Enabled = false;
                            teamTwoScoreTextBox.Enabled = false;
                        }
                        else
                        {
                            scoreButton.Enabled = true;
                            teamOneScoreTextBox.Enabled = true;
                            teamTwoScoreTextBox.Enabled = true;

                            teamOneScoreTextBox.Text = "0";
                            teamTwoScoreTextBox.Text = "0";
                        }
                    }

                    

                    if (currMatchup.Entries[0].TeamCompeting == null)
                    {
                        teamOneLabel.Text = "Not yet determined";
                        scoreButton.Enabled = false;
                        teamOneScoreTextBox.Enabled = false;
                        teamTwoScoreTextBox.Enabled = false;
                        teamOneScoreTextBox.Text = "0";
                        teamTwoScoreTextBox.Text = "0";
                    }
                    else
                    {
                        teamOneLabel.Text = currMatchup.Entries[0].TeamCompeting.TeamName;
                    }
                    if (currMatchup.Entries[1].TeamCompeting == null)
                    {
                        TeamTwoLabel.Text = "Not yet determined";
                        scoreButton.Enabled = false;
                        teamOneScoreTextBox.Enabled = false;
                        teamTwoScoreTextBox.Enabled = false;
                        teamOneScoreTextBox.Text = "0";
                        teamTwoScoreTextBox.Text = "0";

                    }
                    else
                    {
                        TeamTwoLabel.Text = currMatchup.Entries[1].TeamCompeting.TeamName;
                    }
                    

                } 
            }
        }

        private void refreshMatchupListBox()
        {
            currentRoundMatchups = tournament.Rounds[currRound - 1];
            if (unplyedOnlyCheckBox.Checked)
            {
                displayedMatchups = currentRoundMatchups.FindAll(x => x.Winner == null);
            }
            else
            {
                displayedMatchups = currentRoundMatchups;
            }

            matchupListBox.DataSource = null;
            matchupListBox.DataSource = displayedMatchups;
            matchupListBox.DisplayMember = "Matchup";

        }

        private void roundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currRound = (int)roundComboBox.SelectedItem;
            refreshMatchupListBox();
        }

        private void unplyedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            refreshMatchupListBox();
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currMatchup = (MatchupModel)matchupListBox.SelectedItem;
            refreshScoreDisplay();

        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            double scoreOne;
            double scoreTwo;

            if (Double.TryParse(teamOneScoreTextBox.Text,out scoreOne) && Double.TryParse(teamTwoScoreTextBox.Text,out scoreTwo))
            {
                currMatchup.Entries[0].Score = scoreOne;
                currMatchup.Entries[1].Score = scoreTwo;
                try
                {
                    currMatchup.declareWinner();
                    if (currMatchup.MatchupRound != maxRound)
                    {
                        tournament.advanceWinners(currRound);
                    }
                    else
                    {
                        tournament.completeTournament();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
                refreshMatchupListBox();
            }
            else
            {
                teamOneScoreTextBox.Text = "0";
                teamTwoScoreTextBox.Text = "0";
                MessageBox.Show("Enter score figures", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void backLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dashboard.returnToDashboard();
            this.Close();
            
        }
    }
}
