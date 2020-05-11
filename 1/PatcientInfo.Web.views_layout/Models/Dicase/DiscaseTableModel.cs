using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatcientInfo.Web.Models.Dicase
{
    public class DiscaseTableModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Nazva { get; set; }

        [Display(Name = "Вид хвороби")]
        public string DicaseTypeName { get; set; }

        [Display(Name = "Опис")]
        public string Descripotion { get; set; }
        [Display(Name = "Примітка")]
        public string note { get; set; }
     
        public static explicit operator DiscaseTableModel(Discase obj)
        {
            return new DiscaseTableModel()
            {
                Id = obj.Id,
                Nazva = obj.Nazva,
                DicaseTypeName = obj.DicaseType.Nazva,
                Descripotion = obj.Descripotion,
                note = obj.note,
               
            };
        }
    }
}
