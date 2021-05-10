﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorComponentDemo.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public Values Values { get; set; }
    }

    public class Values
    {
        public int Relativehumidity { get; set; }
        public float Temp { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }

        public int Clouds { get; set; }
        public float Uv { get; set; }
        public int Wind_dir { get; set; }
        public float App_temp { get; set; }
    }
}
