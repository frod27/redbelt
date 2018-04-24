using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using redbelt.Models;

namespace redbelt.Models
{
    public class Participant: BaseEntity
    {
        public int ParticipantId{ get; set;}
    
        public int UserId{ get; set;}
        public int IdeaId{ get; set;}
        public User User{ get; set;}
        public Idea Idea{ get; set;}
    }
}