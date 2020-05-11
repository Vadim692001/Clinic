using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatcientInfo.Web.Models.Dicase
{
    public class DiscacaseEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва хвороби")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Назва хвороби\'")]
        [StringLength(30, MinimumLength = 3,
             ErrorMessage = "Назва хвороби  "
             + "повинна містити від 3 до 30 символів")]
        public string Nazva { get; set; }

        [Display(Name = "Види хвороби")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Вид хвороби\'")]
        [StringLength(50, MinimumLength = 3,
             ErrorMessage = "Вид хвороби пацієнта  "
             + "повинна містити від 3 до 50 символів")]
        public string DicaseTypeName { get; set; }

        [Display(Name = "Опис")]
        
        [StringLength(100, MinimumLength = 0,
             ErrorMessage = "Опис  "
             + "повинен містити від 0 до 100 символів")]
        public string Descripotion { get; set; }
        [Display(Name = "примітка ")]
        [StringLength(50, MinimumLength = 0,
             ErrorMessage = "примітка  "
             + "повинна містити від 0 до 50 символів")]
        public string note { get; set; }
     



        public static explicit operator DiscacaseEditingModel(Discase obj)
        {
            return new DiscacaseEditingModel()
            {
                Id = obj.Id,

                Nazva = obj.Nazva,
                DicaseTypeName = obj.DicaseType.Nazva,
                Descripotion = obj.Descripotion,
                note = obj.note
            };
        }
    }
}