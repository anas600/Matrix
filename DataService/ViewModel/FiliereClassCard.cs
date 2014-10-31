﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Model;

namespace DataService.ViewModel
{
    public class FiliereClassCard
    {

        public FiliereClassCard ( Filiere Fil )
        {
            FILIERE_ID = Fil.FILIERE_ID;
            FILIERE_NAME = Fil.NAME.ToUpper();

            CLASS_LIST = new List<ClassCard> ();
            GetCLASS_LIST ();           
        }

        public string FILIERE_ID { get; set; }

        public string FILIERE_NAME { get; set; }

        public List<ClassCard> CLASS_LIST { get; set; }


        #region HELPERS

        private void GetCLASS_LIST ()
        {
            using(var Db = new EF ())
            {                
                foreach (var CL in Db.CLASSE.Where(C => C.FILIERE_ID == FILIERE_ID))
                {
                    CLASS_LIST.Add (new ClassCard (CL));
                }
            }
        }
       
        #endregion

    }
}