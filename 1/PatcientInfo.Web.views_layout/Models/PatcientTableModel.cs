using PatcientInfo.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatcientInfo.Web.Models
{
    public class PatcientTableModel
    {
        public int Id { get; set; }

        [Display(Name = "Прізвище")]
        public string Sorname { get; set; }

        [Display(Name = " Хвороба")]
        public string DicaseName { get; set; }

        [Display(Name = "Лікар")]
        public string Doctor { get; set; }
        [Display(Name = "Медична картка")]
        public string Medical_card { get; set; }
        [Display(Name = "Дата")]
        public string Date { get; set; }
        public static explicit operator PatcientTableModel(Patcient obj)
        {
            return new PatcientTableModel()
            {
                Id = obj.Id,
                Sorname = obj.Sorname,
                DicaseName = obj.Dicase.Nazva,
                Doctor = obj.Doctor,
                Medical_card = obj.Medical_card,
                Date = obj.Date.ToString()
            };
        }
    }
}
