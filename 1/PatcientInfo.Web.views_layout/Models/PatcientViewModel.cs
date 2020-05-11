using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatcientInfo.Web.Models
{
    public class PatcientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Прізвище")]
        public string Sorname { get; set; }

        [Display(Name = "Вид хвороби")]
        public string DicaseName { get; set; }

        [Display(Name = "Лікарь")]
        public string Doctor { get; set; }
        [Display(Name = "Медична картка")]
        public string Medical_card { get; set; }
        [Display(Name = "Дата")]
        public string Date { get; set; }
        [ScaffoldColumn(false)]
        public bool HasInfo { get; set; }


        public static explicit operator PatcientViewModel(Patcient obj)
        {
            return new PatcientViewModel()
            {
                Id = obj.Id,
                Sorname = obj.Sorname,
                DicaseName = obj.Dicase.Nazva,
                Doctor = obj.Doctor,
                Medical_card = obj.Medical_card,
                Date = obj.Date.ToString(),
                HasInfo = !string.IsNullOrWhiteSpace(obj.Note)
                    || !string.IsNullOrWhiteSpace(obj.Description)
            };
        }
    }
}