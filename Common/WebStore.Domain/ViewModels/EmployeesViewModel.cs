using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels
{
    public class EmployeesViewModel //: IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина строи имени должна быть от 3 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата имени. Либо русские буквы, либо латиница. Никаких цифр!")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательным")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина строи фамилии должна быть от 3 до 200 символов")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст является обязательным")]
        [Range(20, 80, ErrorMessage = "Возраст должен быть в пределах от 20 до 80 лет")]
        public int Age { get; set; }

        [Display(Name = "Дата начала трудового договора")]
        [DataType(DataType.DateTime)]
        public DateTime EmployementDate { get; set; }
    }
}
