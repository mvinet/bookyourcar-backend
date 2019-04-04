﻿using System;
using System.Collections.Generic;

namespace TestAuthentification.Models
{
    public partial class Pole
    {
        public Pole()
        {
            LocationLocPoleIdendNavigation = new HashSet<Location>();
            LocationLocPoleIdstartNavigation = new HashSet<Location>();
            Ride = new HashSet<Ride>();
            User = new HashSet<User>();
            Vehicle = new HashSet<Vehicle>();
        }

        public int PoleId { get; set; }
        public string PoleName { get; set; }
        public string PoleCity { get; set; }
        public string PoleAddress { get; set; }
        public string PoleCp { get; set; }

        public ICollection<Location> LocationLocPoleIdendNavigation { get; set; }
        public ICollection<Location> LocationLocPoleIdstartNavigation { get; set; }
        public ICollection<Ride> Ride { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
