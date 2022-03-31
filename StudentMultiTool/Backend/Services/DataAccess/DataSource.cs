using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMultiTool.Backend.Services.DataAccess
{
    public interface DataSource<T>
    {
        // Method for reading data, return 0 for sucessful operation
        Object ReadData(T model);

        // Method for Writing data to a data source
        bool WriteData(T model);

        //Method for deleteing data from a data source, 0 for successful
        bool DeleteData(T model);

        // Method for updating data from a data source, 0 for sucessful
        bool UpdateData(T model);
    }
}
