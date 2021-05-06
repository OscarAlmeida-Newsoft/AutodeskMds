using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Models.Recommendations
{
    public enum Booleano
    {
        False = 0,
        True = 1,
        Both = 2
    }

    public enum Field
    {
        Normal = 0,
        Cal = 1,
        Both = 2
    }

    public enum VariableType
    {
        Primary = 0,
        Compound = 1,
        CustomerAttribute = 2
    }

    public class CreateUpdateVariableViewModel
    {
        public short VariableID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Type")]
        public VariableType Type { get; set; }

        [Display(Name = "Product families")]
        public bool SelectAllFamilies { get; set; }
        public SelectList FamiliesList { get; set; }
        public List<string> families { get; set; }

        [Display(Name = "Products")]
        public bool SelectAllProducts { get; set; }
        public SelectList ProductList { get; set; }
        public List<string> products { get; set; }

        [Display(Name = "Operator")]
        public string Selector { get; set; }
        public SelectList SelectorList { get; set; }

        [Display(Name = "Generate Expression")]
        public string VariableSelector { get; set; }
        public SelectList VariableList { get; set; }


        [Display(Name = "License Type")]
        public Field Field { get; set; }

        [Display(Name = "Corporate")]
        public Booleano IsCorporate { get; set; }

        [Display(Name = "Commercial")]
        public Booleano IsCommercial { get; set; }

        [Display(Name = "Supported")]
        public Booleano IsSupported { get; set; }
        
        [Display(Name = "Attribute")]
        public string CustomerVariable { get; set; }

        public bool? IsMathExpression { get; set; }

        public string MathExpression { get; set; }
        
        public string[] VariableFuntionList { get; set; }

        public bool IsEditing { get; set; }

    }
}