﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataService.Entities.Pedagogy;

namespace Matrix.views.Pedagogy
{
    
    //todo Consider Abandoning Matieres Entity

    /// <summary>
    /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe donnee 
    /// </summary>
    public partial class ClassDetails
    {
        //private string _currentSelected;
        private Classe _openedClass = new Classe();

        /// <summary>
        /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe 
        /// </summary>
        /// <param name="openClassGuid"> ID De la Classe</param>
        public ClassDetails(Guid openClassGuid)
        {
            InitializeComponent();

            _openedClass.ClasseGuid = openClassGuid;

            new Task(() =>
            {
                _openedClass = App.DataS.Pedagogy.Classes.GetClasseById(openClassGuid);
                Dispatcher.BeginInvoke(new Action(() => { CLASS_FILIERE.Text = App.DataS.Pedagogy.Filieres.GetFiliereById(_openedClass.FiliereGuid).Name.ToUpper(); }));
                Dispatcher.BeginInvoke(new Action(() => { CLASS_NAME.Text = _openedClass.Name.ToUpper(); }));
            }).RunSynchronously();

        }

        private void UpdateData()
        {                                  
            Parallel.Invoke(
                () =>  CLASS_WEEK_SCHEDULE.Refresh(_openedClass.ClasseGuid),

                () => CLASSE_MATIERES.Refresh(_openedClass.ClasseGuid),

                () => CLASSE_STUDENTS.Refresh(_openedClass.ClasseGuid, App.DataS.Pedagogy.CurrentAnneeScolaireGuid)
            );
        }


        #region EVENT HANDLERS

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new PedagogyView());
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("AddContext") as ContextMenu;
            if (cm == null) return;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void AddCours_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddCours(_openedClass.ClasseGuid) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddMatiere_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddMatiere(_openedClass.ClasseGuid) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddInscription_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddInscription(_openedClass.ClasseGuid.ToString()) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddAnneScolaire_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddAnneeScolaire { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            NavigationService?.Refresh();           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //if(CurrentSelected == null)
            //{
            //    MessageBox.Show ("Selectionner Une Matiere A Supprimer !");
            //    return;
            //}

            //var theGaName = App.Db.GetMatiereName (CurrentSelected);
            //theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            //if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            //MessageBox.Show (App.Db.DeleteMatiere (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
            //UpdateMatieres ();
        }

       
       

        #endregion
       
        
       
    }
}












//Parallel.Invoke(
//    () => Dispatcher.BeginInvoke(new Action(() => { CLASS_WEEK_SCHEDULE.Refresh(_openedClass.ClasseGuid); })),

//    () => Dispatcher.BeginInvoke(new Action(() => { CLASSE_MATIERES.Refresh(_openedClass.ClasseGuid); })),

//    () => Dispatcher.BeginInvoke(new Action(() => { CLASSE_STUDENTS.Refresh(_openedClass.ClasseGuid, App.DataS.Pedagogy.CurrentAnneeScolaireGuid); }))
//);


//new Task(() =>
//{               
//    Dispatcher.BeginInvoke(new Action(() => { CLASS_WEEK_SCHEDULE.UpdateData(_openedClass.ClasseGuid); }));
//    Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(_openedClass.ClasseGuid); }));
//    Dispatcher.BeginInvoke(new Action(() => { CLASSE_STUDENTS.Refresh(_openedClass.ClasseGuid, App.DataS.Pedagogy.CurrentAnneeScolaireGuid); }));

//}).Start();


//Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(OpenedClass.CLASSE_ID); }));
//Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(OpenedClass.CLASSE_ID); }));
//Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.GetClassStudents(OpenedClass.CLASSE_ID); }));