﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.DbUp.Services.Abstractions
{
    public interface IDatabaseMigrationService
    {
        void ApplyMSSQLDatabaseMigrations();
    }
}
