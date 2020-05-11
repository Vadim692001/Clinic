using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatcientInfo.Web.Models
{
    public class PatcientInfoModel
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
        public string[] Info { get; private set; }

        private static string[] CreateInfo(Patcient obj)
        {
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.Note))
            {
                s += "Примітка: " + obj.Note + "\n";
            }
            if (!string.IsNullOrWhiteSpace(obj.Description))
            {
                s += "Опис\n" + obj.Description;
            }
            string[] info = null;
            if (s != null)
            {
                info = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
            return info;
        }

        public static explicit operator PatcientInfoModel(Patcient obj)
        {
            return new PatcientInfoModel()
            {
                Id = obj.Id,
                Sorname = obj.Sorname,
                DicaseName = obj.Dicase.Nazva,
                Doctor = obj.Doctor,
                Medical_card = obj.Medical_card,
                Date = obj.Date.ToString(),
                Info = CreateInfo(obj)
            };
        }
    }
}