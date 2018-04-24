using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using redbelt;
using redbelt.Models;

namespace redbelt.Models
{
    public class AddIdeaViewModel: BaseEntity
    {
        [Required(ErrorMessage="Help a human understand by writing a little more.")]
        [MinLength(1)]
        public string Description { get; set; }
        }
    }