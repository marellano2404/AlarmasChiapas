using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AlarmasWPF.Core.ViewModels
{
    public class DbReportContext : DbContext
    {
        public DbSet<ReportViewModel> ReportViewModel { get; set; }

       
    }  
}
