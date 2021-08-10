using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Models;
using TrackerLibrary;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        private List<TeamModel> availableTeams;
        private List<TeamModel> selectedTeams = new List<TeamModel>();
        private List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        private IReturnToDashboard dashboard;

        public CreateTournamentForm(IReturnToDashboard dash)
        {
            InitializeComponent();
            availableTeams = GlobalConfig.Connection.getTeamAll();
            dashboard = dash;
            refreshData();
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)selectTeamComboBox.SelectedItem;

            if (team != null)
            {
                availableTeams.Remove(team);
                selectedTeams.Add(team);
                refreshData();
            }
        }

        private void removePlayersButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)tournamentPlayersListBox.SelectedItem;

            if (team != null)
            {
                selectedTeams.Remove(team);
                availableTeams.Add(team);
                refreshData();
            }
        }

        private void removePrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel prize = (PrizeModel)prizesListBox.SelectedItem;

            if (prize != null)
            {
                selectedPrizes.Remove(prize);
                refreshData();

            }

        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            CreateprizeForm form = new CreateprizeForm(this);

            form.Show();
        }

        private void createNewLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm form = new CreateTeamForm(this);
            form.Show();

        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tour = new TournamentModel();
            bool isValid;
            try
            {
                isValid = validateTournament();
            }
            catch (Exception ex)
            {
                isValid = false;
                MessageBox.Show(ex.Message);
            }

            if (isValid)
            {
                tour.TournamentName = tournamentNametextBox.Text;
                tour.EntryFee = Decimal.Parse(entryFeeTextBox.Text);
                tour.EnteredTeams = selectedTeams;
                tour.Prizes = selectedPrizes;
                tour.Active = 1;

                TournamentLogic.createRounds(tour);

                GlobalConfig.Connection.createTournament(tour);

                TournamentViewerForm tournamentViewerForm = new TournamentViewerForm(tour, dashboard);
                tournamentViewerForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid form");
            }

            

        }

        private bool validateTournament()
        {
            decimal fee = 0;
            bool output = true;

            if (tournamentNametextBox.Text.Length < 1)
            {
                output = false;
            }
            if (Decimal.TryParse(entryFeeTextBox.Text,out fee))
            {
                if (fee < 1)
                {
                    output = false;
                }
            }
            else
            {
                output = false;
            }
            if (selectedTeams.Count < 2)
            {
                throw new Exception("Tournaments need at least two teams");
            }

            return output;
        }

        private void refreshData()
        {
            selectTeamComboBox.DataSource = null;
            selectTeamComboBox.DataSource = availableTeams;
            selectTeamComboBox.DisplayMember = "TeamName";

            tournamentPlayersListBox.DataSource = null;
            tournamentPlayersListBox.DataSource = selectedTeams;
            tournamentPlayersListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";


        }

        public void prizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
            refreshData();
        }

        public void teamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            refreshData();
        }

        
    }
}
