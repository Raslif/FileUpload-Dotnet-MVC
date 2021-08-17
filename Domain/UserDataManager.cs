using Data;
using Data.Access;
using Domain.Models.Command;
using Domain.Models.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Domain
{
    public class UserDataManager
    {
        private UserDataAccessManager userDataAccessManager = null;
        public UserDataManager()
        {
            userDataAccessManager = new UserDataAccessManager();
        }

        public async Task<bool> SaveUserDetails(UserSaveModel userSaveModel)
        {
            if (userSaveModel == null)
                throw new Exception("User Details null.");

            var userSaveDBModel = GetUserSaveModel(userSaveModel);

            return await userDataAccessManager.SaveUserDetails(userSaveDBModel);
        }

        public async Task<List<UserDetailsModel>> GetUserDetails()
        {
            var listOfUsers = await userDataAccessManager.GetUserDetails();
            var listOfUserDetails = new List<UserDetailsModel>();
            foreach (var user in listOfUsers)
            {
                listOfUserDetails.Add(new UserDetailsModel()
                {
                    FName = user.FName,
                    LName = user.LName,
                    DOB = user.DOB.Date.ToString("dddd, dd MMMM yyyy"),
                    PhotoName = user.ProfilePhotoName,
                    PhotoPath = user.ProfilePhotoPath,
                    DocName = user.DocumentName,
                    DocPath = user.DocumentPath
                });
            }

            return listOfUserDetails;
        }

        private UserData GetUserSaveModel(UserSaveModel userSaveModel)
        {
            string photoFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/UserFiles/Images/");
            string docFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/UserFiles/Docs/");

            var photoFileName = userSaveModel.FName + "_" + userSaveModel.ProfilePicture.FileName;
            string photoSavePath = Path.Combine(photoFolderPath, photoFileName);
            userSaveModel.ProfilePicture.SaveAs(photoSavePath);

            var docFileName = userSaveModel.FName + "_" + userSaveModel.Document.FileName;
            string docSavePath = Path.Combine(docFolderPath, docFileName);
            userSaveModel.Document.SaveAs(docSavePath);

            return new UserData()
            {
                FName = userSaveModel.FName,
                LName = userSaveModel.LName,
                DOB = DateTime.Now,
                ProfilePhotoName = photoFileName,
                ProfilePhotoPath = Path.Combine("/UserFiles/Images/", photoFileName),
                DocumentName = docFileName,
                DocumentPath = Path.Combine("/UserFiles/Docs/", docFileName)
            };
        }
    }
}
