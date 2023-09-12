using DifferentLearn.Data.Entites.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group
        Task<List<CourseGroup>> GetAllGroup();
        #endregion
    }
}
