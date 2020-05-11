using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PatcientInfo.Entities;

namespace PatcientInfo.Web.Models.Dicase
{
    public class DicaseInfoModel
    {
        
            public int Id { get; set; }

            [Display(Name = "Назва")]
            public string Nazva { get; set; }

            [Display(Name = " Вид хвороби")]
            public string DicaseTypeName { get; set; }

        
           
            public string[] Info { get; private set; }

            private static string[] CreateInfo(Discase obj)
            {
                string s = null;
                if (!string.IsNullOrWhiteSpace(obj.note))
                {
                    s += "Примітка: " + obj.note + "\n";
                }
                if (!string.IsNullOrWhiteSpace(obj.Descripotion))
                {
                    s += "Опис\n" + obj.Descripotion;
                }
                string[] info = null;
                if (s != null)
                {
                    info = s.Split(new[] { '\n' },
                        StringSplitOptions.RemoveEmptyEntries).ToArray();
                }
                return info;
            }

            public static explicit operator DicaseInfoModel(Discase obj)
            {
                return new DicaseInfoModel()
                {
                    Id = obj.Id,
                    Nazva = obj.Nazva,
                    DicaseTypeName = obj.DicaseType.Nazva,
            
                    Info = CreateInfo(obj)
                };
            }
        }
    }
