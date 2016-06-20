﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class CondoModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParkingSlots { get; set; }

        public List<TowerModel> Towers { get; set; }
    }
}