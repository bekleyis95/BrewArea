﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewArea.DAL.Repsitory;
using BrewArea.COM;

namespace BrewArea.BUS.Service
{
   public class MemberService
    {
        MemberRepo mrp;
        IngredientRepo irp;

        public MemberService()
        {
            mrp = new MemberRepo();
            irp = new IngredientRepo();
        }

        public MemberViewModel GetByUsername( string username)
        {
            var user = mrp.GetByUsername(username);
            if (user != null)
            {
                var returnItem = new MemberViewModel
                {
                    Username = user.Username,
                    Password = user.Password,
                    MemberType = user.MemberTypeId
                };
                return returnItem;
            }
           
            return null;
        }

        public bool CreateMember(MemberViewModel member)
        {
            try
            {
                mrp.Create(new Member
                {
                    Username = member.Username,
                    Password = member.Password,
                    MemberTypeId = 3
                 
                });

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
      
        }

        public List<IngredientViewModel> GetAllIngredientsofMember(string username)
        {
            //Do validation here

            var memberId = mrp.GetByUsername(username).MemberId;


            return irp.GetMemberIngredients(memberId);
        }
    }
}
