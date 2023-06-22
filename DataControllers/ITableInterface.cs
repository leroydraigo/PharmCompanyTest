using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmCompany.DataControllers
{
    public interface ITableInterface
    {
        void Create();
        void Delete();
        void Show();
    }
}
