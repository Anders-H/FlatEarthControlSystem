﻿using System;
using System.Collections.Generic;
using System.Text;
using FlatEarthControlSystem;
using FlatEarthControlSystem.WorldDefinition;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class InventoryTests
    {
        [Fact]
        public void CanUseShortestAvailableName()
        {
            var flatEarth = new FlatEarth();

            var i1 = new Inventory(flatEarth);

            
        }
    }
}