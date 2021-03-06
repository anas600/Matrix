﻿using System;
using System.Windows;
using System.Windows.Media;
using DataService.Entities.Pedagogy;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    /// <summary>
    /// Form Pour Ajouter Une Matiere
    /// </summary>
    public partial class AddMatiere
    {
        private readonly bool _isAdd;               
        private readonly Matiere _matiereDisplayed = new Matiere();

        /// <summary>
        /// Form pour Ajouter/Modifier une Matiere
        /// </summary>
        /// <param name="currentClassGuid">ID</param>
        /// <param name="matiereToDisplay">Object</param>
        public AddMatiere ( Guid currentClassGuid, Matiere matiereToDisplay = null )
        {
            InitializeComponent ();
            
            _matiereDisplayed.ClasseGuid = currentClassGuid;

            if(matiereToDisplay == null)
            {
                _isAdd = true;
                _matiereDisplayed.MatiereGuid = Guid.NewGuid ();               
            }
            else
            { 
                _isAdd = false;               
                _matiereDisplayed = matiereToDisplay;
                DisplayMatiere (_matiereDisplayed);                                
            }                                     
        }
        
        private void DisplayMatiere(Matiere matiereToDisplay)
        {
            if(matiereToDisplay == null) return;

            TITLE_TEXT.Text = "MODIFICATION";
            MATIERE_NAME.Text = matiereToDisplay.Name;
            SIGLE.Text = matiereToDisplay.Sigle;
            COEFFICIENT.Value = matiereToDisplay.Coefficient;
            // ReSharper disable once PossibleNullReferenceException
            COULEUR.SelectedColor = (Color)ColorConverter.ConvertFromString (matiereToDisplay.Couleur);
            DESCRIPTION.Text =matiereToDisplay.Description;                         
        }
       
        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

            if(ChampsValidated () != true) return;
            
            _matiereDisplayed.Name = MATIERE_NAME.Text.Trim ();            
            _matiereDisplayed.Sigle = SIGLE.Text;
            _matiereDisplayed.Coefficient = COEFFICIENT.Value.GetValueOrDefault();
            _matiereDisplayed.Couleur = COULEUR.SelectedColorText;
            _matiereDisplayed.Description = DESCRIPTION.Text;          

            if(_isAdd)
            {
                try
                {
                    App.DataS.Pedagogy.Matieres.AddMatiere(_matiereDisplayed);
                    ModernDialog.ShowMessage ("Success", "Matrix", MessageBoxButton.OK);                
                }
                catch (Exception ex)
                {                    
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);                   
                }
                Close ();
            }
            else
            {
                try
                {
                    App.DataS.Pedagogy.Matieres.UpdateMatiere(_matiereDisplayed);                    
                    ModernDialog.ShowMessage("Success","Matrix", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);                    
                }                                                                         
                Close ();
            }            
        }

        private bool ChampsValidated()
        {
            var ok = true;

            if(string.IsNullOrEmpty (MATIERE_NAME.Text))
            {
                MATIERE_NAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                MATIERE_NAME.BorderBrush = Brushes.Blue;
            }
                       
            if(!ok) ModernDialog.ShowMessage ("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

                       
    }
}


//private void UpdateMatiereInstructors()
//        {
//            Parallel.ForEach(StaffBuff, S =>
//            {
//                if (S.IsINSTRUCTOR)
//                {
//                    App.DataS.AddMatiereInstructor(MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
//                }
//                else
//                {
//                    App.DataS.DeleteMatiereInstructor(MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
//                }
//            });
//        }

//#region Background Works
       
//        private void UpdateStaffs ( )
//        {
//            if(worker.IsBusy) return;            
//            worker.RunWorkerAsync ();
//        }

//        private void worker_DoWork ( object sender, DoWorkEventArgs e )
//        {
//            StaffBuff = GetStaffModelList (MatiereDisplayed.MATIERE_ID);
//            if (IsAdd) return;
//            Parallel.ForEach(StaffBuff, S =>
//            {
//                S.IsINSTRUCTOR = App.DataS.IsMatiereInstructor(S.STAFF_ID, MatiereDisplayed.MATIERE_ID);
//            });
//        }

//        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
//        {
//            BusyIndicator.IsBusy = false;
//            InstructeursList.Items.Clear();
//            InstructeursList.ItemsSource = StaffBuff;            
//            worker.Dispose ();
//        }

//        private static List<MatiereStaffsModel> GetStaffModelList (Guid MatiereID)
//        {
//            var ML = new List<MatiereStaffsModel>();
//            var Staffs = App.DataS.GetAllStaffs ();

//            Parallel.ForEach(Staffs, S =>
//            {
//                var M = new MatiereStaffsModel
//                {
//                    STAFF_ID = S.STAFF_ID,
//                    FULL_NAME = S.FULL_NAME,
//                    PHOTO_IDENTITY = S.PHOTO_IDENTITY,
//                    QUALIFICATION = S.QUALIFICATION,
//                    IsINSTRUCTOR = App.DataS.IsMatiereInstructor(S.STAFF_ID, MatiereID)
//                };
//                ML.Add(M);
//            });
//            return ML;
//        }


//        #endregion