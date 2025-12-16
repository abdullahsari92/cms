using Core.Extensions;

namespace AS.Core.Helpers
{
    public static class BaseEntityHelper
    {

        public static Guid getGuid()
        {

            return new Guid("00000000-0000-0000-0000-000000000000");                   

        }

        /// <summary>
        /// Entitleri ekleme işlemi yaparken ekleyen,güncelleyen kişi ve tarihlerini set ediyor.
        /// </summary>
        /// <typeparam name="T">Beklenilen entity tipi</typeparam>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static T SetBaseEntitiyNoExten<T>(T Item) where T : IEntity, new()
        {
            //TODO:Bakılacak
            //var activeIdentity = CoreSettings.ActiveIdentity;
            //Item.CreatedById = activeIdentity.UserId;

            Item.CreatedById = UserInfoExtensions.GetUserId();
            Item.UpdatedById = UserInfoExtensions.GetUserId();


            Item.CreationTime = DateTime.Now;
            Item.UpdateTime = DateTime.Now;


            return Item;
        }

        /// <summary>
        /// Entitleri ekleme işlemi yaparken ekleyen,güncelleyen kişi ve tarihlerini set ediyor.
        /// </summary>
        /// <typeparam name="T">Beklenilen entity tipi</typeparam>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static T SetBaseEntitiy<T>(this T Item) where T : IEntity, new()
        {
   
            Item.CreationTime = DateTime.Now;

            Item.CreatedById =UserInfoExtensions.GetUserId();

            return Item;
        }

        public static T SetBaseUpdateEntitiy<T>(this T Item) where T : IEntity, new()
        {
                   
            Item.UpdateTime = DateTime.Now;
            Item.UpdatedById = UserInfoExtensions.GetUserId();


            return Item;
        }


    }
}
