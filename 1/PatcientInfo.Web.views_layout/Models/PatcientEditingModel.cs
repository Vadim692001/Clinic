using PatcientInfo.Entities;
using System.ComponentModel.DataAnnotations;

namespace PatcientInfo.Web.Models
{
    public class PatcientEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Прізвище")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Прізвище\'")]
        [StringLength(30, MinimumLength = 3,
             ErrorMessage = "Прізвище  пацієнта  "
             + "повинна містити від 3 до 30 символів")]
        public string Sorname { get; set; }

        [Display(Name = "Вид хвороби")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Вид хвороби\'")]
        [StringLength(50, MinimumLength = 3,
             ErrorMessage = "Вид хвороби пацієнта  "
             + "повинна містити від 3 до 50 символів")]
        public string DicaseName { get; set; }

        [Display(Name = "Лікарь")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Лікарь\'")]
        [StringLength(20, MinimumLength = 3,
             ErrorMessage = "Лікарь пацієнта  "
             + "повинна містити від 3 до 20 символів")]
        public string Doctor { get; set; }
        [Display(Name = "Медична картка")]
        [Range(1, 5102, ErrorMessage = "Медична картка "
            + "повинно бути в межах від 1 до 5102")]
        public string Medical_card { get; set; }
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Палата")]
        [Range(1, 5102, ErrorMessage = "Палата "
            + "повинно бути в межах від 1 до 5102")]
        public string number_Chamber { get; set; }



        public static explicit operator PatcientEditingModel(Patcient obj)
        {
            return new PatcientEditingModel()
            {
                Id = obj.Id,

                Sorname = obj.Sorname,
                DicaseName = obj.Dicase.Nazva,
                Doctor = obj.Doctor,
                Medical_card = obj.Medical_card,
                Date = obj.Date.ToString(),
                number_Chamber = obj.number_Chamber

            };
        }

        //public static explicit operator Patcient(PatcientEditingModel obj)
        //{
        //    return new Patcient()
        //    {
        //        Id = obj.Id,

        //        Sorname = obj.Sorname,
        //        //Dicase = obj.DicaseName.Clone(),
        //        Doctor = obj.Doctor,
        //        Medical_card = obj.Medical_card,


        //    };
        }
    }
