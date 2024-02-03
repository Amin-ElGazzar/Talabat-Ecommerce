﻿using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.SpecificationClass
{
    public class EmployeesSpecification : BaseSpecification<Employees>
    {
        public EmployeesSpecification()
        {
            Include.Add(e => e.Department);
        }
        public EmployeesSpecification(int id):base(e=>e.Id ==id)
        {
            
        }

    }
}
