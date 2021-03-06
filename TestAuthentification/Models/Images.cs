﻿using System;
using System.Collections.Generic;

namespace TestAuthentification.Models
{
    public partial class Images
    {
        public int ImageId { get; set; }
        public string ImageUri { get; set; }
        public int? ImageVehId { get; set; }
        public int? ImageUserId { get; set; }

        public User ImageUser { get; set; }
        public Vehicle ImageVeh { get; set; }
    }
}
