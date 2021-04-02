using Microsoft.EntityFrameworkCore;
using StrikeNet.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrikeNet.EntityFramework.Interfaces
{
    public interface IStrikeNetDbContext
    {
        DbSet<Game> Games { get; set; }

        DbSet<Prediction> Predictions { get; set; }
    } 
}
