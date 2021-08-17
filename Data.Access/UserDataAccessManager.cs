using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Data.Access
{
    public class UserDataAccessManager
    {
        private UserDetailsEntities userDbContext = null;

        public UserDataAccessManager()
        {
            userDbContext = new UserDetailsEntities();
        }

        public async Task<bool> SaveUserDetails(UserData userData)
        {
            try
            {
                if (userData != null)
                {
                    userDbContext.UserDatas.Add(userData);
                    userDbContext.Entry(userData).State = EntityState.Added;
                    await userDbContext.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<List<UserData>> GetUserDetails()
        {
            try
            {
                return await userDbContext.UserDatas.ToListAsync();
            }
            catch
            {
                throw new Exception(Constants.GetUserDetailsErrorMessage);
            }
        }
    }
}
