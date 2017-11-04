using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core2APIExercise.Data.Enum
{
    public enum IdentifierType
    {
        [Display(Name = "Email")]
        Email = 0
        ,
        [Display(Name = "Access Card")]
        AccessCard
        ,
        [Display(Name = "License Plate")]
        LicensePlate
    }
}
