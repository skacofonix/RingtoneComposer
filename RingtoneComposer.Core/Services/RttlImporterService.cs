﻿using RingtoneComposer.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingtoneComposer.Core.Services
{
    public class RttlImporterService : IRingtoneImporterService
    {
        private RttlConverter converter = new RttlConverter();

        public bool CheckPartitionValitity(string s)
        {
            return converter.CheckPartition(s);
        }
        public Tune Import(string s)
        {
            return converter.Parse(s);
        }
    }
}
