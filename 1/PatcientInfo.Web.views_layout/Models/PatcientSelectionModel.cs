using PatcientInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace PatcientInfo.Web.Models
{
    public class PatcientSelectionModel
    {
        public int Id { get; set; }

        [Display(Name = "Прізвище")]
        public string Sorname { get; set; }

        [Display(Name = "Хвороби")]
        public string DicaseName { get; set; }

        [Display(Name = "Лікарь")]
        public string Doctor { get; set; }
        [Display(Name = "Медична картка")]
        public string Medical_card { get; set; }
        [Display(Name = "Дата")]
        public string Date { get; set; }



        public static explicit operator PatcientSelectionModel(Patcient obj)
        {
            return new PatcientSelectionModel()
            {
                Id = obj.Id,
                Sorname = obj.Sorname,
                DicaseName = obj.Dicase.Nazva,
                Doctor = obj.Doctor,
                Medical_card=obj.Medical_card,
                Date=obj.Date.ToString()
            };
        }
    }
}