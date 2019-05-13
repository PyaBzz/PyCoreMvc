﻿using System;
using System.ComponentModel.DataAnnotations;
using PyaFramework.Core;

namespace myCoreMvc.Domain
{
    public class WorkItem : Thing, IClonable
    {
        public static readonly int[] PriorityChoices = new[] { 1, 2, 3, 4 };

        /*================================  Properties ================================*/

        public String Reference { get; set; }
        public int Priority { get; set; }
        [Display(Name = "Item name"), Required]
        public string Name { get; set; }
        public WorkPlan WorkPlan { get; set; }

        /*==================================  Methods =================================*/

        public override string ToString() => Name;
    }
}
