using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RnD.WPFMVVMSample.Data
{
    public interface IDatabaseFactory : IDisposable
    {
        AppDbContext Get();
    }
}
