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
    public partial class TournamentDashboardForm : Form, IReturnToDashboard 
    {
        List<TournamentModel> tournaments = GlobalConfig.Connection.getTournamentAll();

        public TournamentDashboardForm()
        {
            InitializeComponent();
            refreshData();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tournament = (TournamentModel)loadExistingTournamentComboBox.SelectedItem;
            TournamentViewerForm tournamentViewerForm = new TournamentViewerForm(tournament, this);
            tournamentViewerForm.Show();
            this.Hide();
        }

        private void refreshData()
        {
            loadExistingTournamentComboBox.DataSource = null;
            loadExistingTournamentComboBox.DataSource = tournaments;
            loadExistingTournamentComboBox.DisplayMember = "TournamentDisplay";
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm createTournamentForm = new CreateTournamentForm(this);
            createTournamentForm.Show();
            this.Hide();
        }

        public void returnToDashboard()
        {
            this.Show();
            tournaments = GlobalConfig.Connection.getTournamentAll();
            refreshData();
        }
    }
}
