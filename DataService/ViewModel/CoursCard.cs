﻿using System;
using DataService.Context;
using DataService.Entities.Pedagogy;

namespace DataService.ViewModel
{
    /// <summary>
    /// Les Informations d'un cours
    /// </summary>
    public class CoursCard
    {
        /// <summary>
        /// Les Informations s'un cours
        /// </summary>
        /// <param name="currentCous">Le Cours</param>
        /// <param name="coursDay">La Journee De Recurrence</param>
        public CoursCard ( Cours currentCous, DateTime coursDay )
        {
            //Type = currentCous.Type.ToUpper ();
            Type = currentCous.Type.ToString().ToUpper();
            CoursGuid = currentCous.CoursGuid;           
            
            Salle = currentCous.Salle.ToUpper();

            CoursDate = coursDay;

            StartTime = currentCous.StartTime;
            EndTime = currentCous.EndTime;

            ForeColor = "Black";
           
            ResolveData (coursDay, currentCous.MatiereGuid, currentCous.StaffGuid);
            
        }

        /// <summary>
        /// ID du cours
        /// </summary>
        public Guid CoursGuid { get;  }

        /// <summary>
        /// Nom de la matiere
        /// </summary>
        public string MatiereName { get; set; }

        /// <summary>
        /// Le Type du Cours: Cours, Control, Test, TP, TD Etc..
        /// </summary>
        public string Type { get; set; } 

        /// <summary>
        /// Le nom de l'instructeur
        /// </summary>
        public string StaffFullName { get; set; }
        
        /// <summary>
        /// La Salle ou le cours Sera Dispenser
        /// </summary>
        public string Salle { get; set; }
        
        /// <summary>
        /// La Journee ou le cours sera dispenser
        /// </summary>
        public DateTime CoursDate { get; set; }

        /// <summary>
        /// La couleur du cours
        /// </summary>
        public string Couleur { get; set; }

        /// <summary>
        /// Forecolor
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// L'heure ou le cours commencera 
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// L'heure ou le cours terminera 
        /// </summary>
        public TimeSpan EndTime { get; set; }

        private void ResolveData (DateTime coursDay, Guid matiereGuid, Guid staffGuid )
        {           
            using(var db = new Ef ())
            {
                var m = db.Matiere.Find(matiereGuid);

                MatiereName = m.Name.Substring(0,1).ToUpper() + m.Name.Substring(1).ToLower();
                Couleur = m.Couleur;

                StaffFullName = db.Staff.Find (staffGuid).FullName;
            }

            if (coursDay < DateTime.Today)
            {
                ForeColor = "Gray";
            }
            else if(coursDay == DateTime.Today)
            {
                if (EndTime < DateTime.Now.TimeOfDay) ForeColor = "Gray";
               
                if (StartTime <= DateTime.Now.TimeOfDay && EndTime >= DateTime.Now.TimeOfDay) ForeColor = "Red";
              
            }
                      
        }

    }
}
