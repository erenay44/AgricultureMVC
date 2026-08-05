using DataLayer.Abstract;
using DataLayer.Concrete.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete.EntityFramework
{
    public class EfAboutDal: GenericRepository<About>, IAboutDal
    {
    }
}
