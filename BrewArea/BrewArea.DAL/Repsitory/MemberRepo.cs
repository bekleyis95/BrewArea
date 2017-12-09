using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewArea.COM;

namespace BrewArea.DAL.Repsitory
{
    public class MemberRepo
    {
        public Member GetById(int id)
        {
            using (var ctx = new BrewAreaEntities())
            {
 
                return ctx.Members.ToList().SingleOrDefault(t => t.MemberId == id);
            }
        }
    }
}
