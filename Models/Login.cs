using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using redbelt;
using redbelt.Models;

namespace redbelt.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public List<Participant> Participants { get; set; }

        public List<Activity> Activities { get; set; }
        public User() {
            Participants = new List<Participant>();
            Activities = new List<Activity>();
        }
    }
}