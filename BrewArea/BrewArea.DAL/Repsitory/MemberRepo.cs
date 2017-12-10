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
        public Member GetByUsername(string username)
        {
            using(var ctx = new BrewAreaEntities())
            {
                return ctx.Members.Where(t => t.Username == username).SingleOrDefault();
            }
        }

        public bool Create(Member member)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    ctx.Members.Add(member);
                    ctx.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
    }
}
