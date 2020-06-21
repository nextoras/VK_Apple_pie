using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vk_server
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        

        IRepository <User> Users { get;  }
        IRepository <OAuthClientDetail> OAuthClientDetails { get; }
        IRepository <PhotoHuman> PhotoHumans { get; }
        IRepository <UserParameter> UserParameters { get; }
        IRepository <Clothing> Clothings { get; }
        IRepository <Sex> Sexs { get; }
        IRepository <Basket> Baskets { get; }
        IRepository <ClothingSize> ClothingSizes { get; }
        IRepository <Shop> Shops { get; }
        IRepository <RenderPhoto> RenderPhotos { get; }
        IRepository <Size> Sizes { get; }
        

        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
