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
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availablePeople;
        private List<PersonModel> selectedPeople = new List<PersonModel>();
        ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
            availablePeople = GlobalConfig.Connection.getPeopleAll();
            //sampleData();

            selectMemberComboBox.DataSource = availablePeople;
            selectMemberComboBox.DisplayMember = "FullName";

            teamMembersListBox.DataSource = selectedPeople;
            teamMembersListBox.DisplayMember = "FullName";
        }
        /// <summary>
        /// Sample data for testing
        /// </summary>
        private void sampleData()
        {
            availablePeople.Add(new PersonModel() { FirstName = "Wesley", LastName = "Emuobonuvie" });
            availablePeople.Add(new PersonModel() { FirstName = "Kevin", LastName = "Ahwin" });
            availablePeople.Add(new PersonModel() { FirstName = "Tim", LastName = "Corey" });
            availablePeople.Add(new PersonModel() { FirstName = "Jhonny", LastName = "Storm" });
        }


        private void selectMemberButton_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Event happens when the create member button is clicked
        /// It validates and writes the data in the create member group box to the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel person = new PersonModel();

            if (validateCreateMember())
            {
                person.FirstName = firstNameTextBox.Text;
                person.LastName = lastNameTextBox.Text;
                person.Email = eMailTextBox.Text;
                person.CellPhoneNumber = phoneNumberTextBox.Text;

                GlobalConfig.Connection.createPerson(person);

                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                eMailTextBox.Text = "";
                phoneNumberTextBox.Text = "";

                selectedPeople.Add(person);
                refreshData();

            }


        }
        
        /// <summary>
        /// Moves the selected person from the select member combo box 
        /// to the team member list box when the add member button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel person = new PersonModel();
            person = (PersonModel)selectMemberComboBox.SelectedItem;
            if (person != null)
            {
                selectedPeople.Add(person);
                availablePeople.Remove(person);
                refreshData();
            }
            
            
        }

        
        /// <summary>
        /// Moves selected person from the team member list box to the 
        /// select member combo box when the remove player button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removePlayersButton_Click(object sender, EventArgs e)
        {
            PersonModel person = new PersonModel();
            person = (PersonModel)teamMembersListBox.SelectedItem;
            if (person != null)
            {
                selectedPeople.Remove(person);
                availablePeople.Add(person);
                refreshData();
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = new TeamModel();

            if (validateCreateTeam())
            {
                team.TeamName = teamNameTextBox.Text;
                team.TeamMembers = selectedPeople;

                team = GlobalConfig.Connection.createTeam(team);

                callingForm.teamComplete(team);
                this.Close();
            }
            
        }

        /// <summary>
        /// Validates create team form
        /// </summary>
        /// <returns>True if data is valid</returns>
        private bool validateCreateTeam()
        {
            bool output = true;

            if (selectedPeople.Count <= 0)
            {
                output = false;
            }
            if (teamNameTextBox.Text.Length <= 0)
            {
                output = false;
            }

            return output;
        }


        /// <summary>
        /// Validates the data in the create member Group box
        /// </summary>
        /// <returns>True if data is valid</returns>
        private bool validateCreateMember()
        {
            bool output = true;

            if (firstNameTextBox.Text.Length < 1)
            {
                output = false;
            }

            if (lastNameTextBox.Text.Length < 1)
            {
                output = false;
            }

            if (eMailTextBox.Text.Length < 1)
            {
                output = false;
            }

            if (phoneNumberTextBox.Text.Length < 1)
            {
                output = false;
            }

            return output;
        }

        /// <summary>
        /// refreshes the data in the select member combo box and 
        /// the team member list box
        /// </summary>
        private void refreshData()
        {
            selectMemberComboBox.DataSource = null;
            selectMemberComboBox.DataSource = availablePeople;
            selectMemberComboBox.DisplayMember = "FullName";
            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedPeople;
            teamMembersListBox.DisplayMember = "FullName";
        }

        
    }
}
