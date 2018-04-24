using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using redbelt;

namespace redbelt.Models
{
    public class Idea: BaseEntity
    {
        public int IdeaId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string Description { get; set; }
        public List<Participant> participants { get; set; }
        public Idea() {
            participants = new List<Participant>();
        }
    }
}